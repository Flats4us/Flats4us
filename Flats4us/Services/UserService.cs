using AutoMapper;
using Azure.Core;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers;
using Flats4us.Helpers.Enums;
using Flats4us.Helpers.Exceptions;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Flats4us.Services
{
    public class UserService : IUserService
    {
        public readonly Flats4usContext _context;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IFileUploadService _fileUploadService;

        public UserService(Flats4usContext context,
            IEmailService emailService,
            IMapper mapper,
            IConfiguration configuration,
            INotificationService notificationService,
            IFileUploadService fileUploadService)
        {
            _context = context;
            _emailService = emailService;
            _mapper = mapper;
            _configuration = configuration;
            _notificationService = notificationService;
            _fileUploadService = fileUploadService;
        }

        public async Task<TokenDto> AuthenticateAsync(string email, string password, string fcmToken = null)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) throw new AuthenticationException("Invalid email or password.");

            user.LastLoginDate = DateTime.Now;

            // Save FCM token if provided
            if (!string.IsNullOrEmpty(fcmToken))
            {
                user.FcmToken = fcmToken;
            }
            await _context.SaveChangesAsync();

            var token = CreateToken(user);

            return token;
        }

        public async Task<TokenDto> RegisterOwnerAsync(OwnerRegisterDto input)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(x => x.Email == input.Email);

            if (existingUser != null) throw new Exception("Email already exists");

            if (input.Password.Length < 8 || input.Password.Length > 50) throw new Exception("Password must be between 8 and 50 characters");

            if (!input.Password.Any(char.IsUpper) || !input.Password.Any(char.IsLower) || !input.Password.Any(char.IsDigit)) throw new Exception("Password must contain at least one uppercase letter, one lowercase letter, and one digit");
            
            var owner = _mapper.Map<Owner>(input);

            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();

            var token = CreateToken(owner);

            return token;
        }

        public async Task<TokenDto> RegisterStudentAsync(StudentRegisterDto input)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(x => x.Email == input.Email);

            if (existingUser != null) throw new Exception("Email already exists");

            if (input.Password.Length < 8 || input.Password.Length > 50) throw new Exception("Password must be between 8 and 50 characters");

            if (!input.Password.Any(char.IsUpper) || !input.Password.Any(char.IsLower) || !input.Password.Any(char.IsDigit)) throw new Exception("Password must contain at least one uppercase letter, one lowercase letter, and one digit");

            var student = _mapper.Map<Student>(input);

            var studentSurvey =_mapper.Map<SurveyStudent>(input);

            student.SurveyStudent = studentSurvey;

            var interestList = await _context.Interests
                .Where(i => input.InterestIds
                    .Contains(i.InterestId)
                )
                .ToListAsync();

            student.Interests = interestList;

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var token = CreateToken(student);

            return token;
        }

        public async Task AddUserFilesAsync(UserFilesDto input, int userId)
        {
            var user = await _context.OwnerStudents
                .Include(u => u.ProfilePicture)
                .Include(u => u.Document)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (user is null) throw new Exception($"Cannot find user ID: {userId}");

            if (input.Document != null)
            {
                user.VerificationStatus = VerificationStatus.NotVerified;

                if (user.Document != null)
                {
                    await _fileUploadService.DeleteFileByNameAsync(user.Document.Name);
                }
                
                user.Document = await _fileUploadService.CreateFileFromIFormFileAsync(input.Document);
            }

            if (input.ProfilePicture != null)
            {
                if (user.ProfilePicture != null)
                {
                    await _fileUploadService.DeleteFileByNameAsync(user.ProfilePicture.Name);
                }
                
                user.ProfilePicture = await _fileUploadService.CreateFileFromIFormFileAsync(input.ProfilePicture);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserFileAsync(string fileId, int userId)
        {
            var user = await _context.OwnerStudents
                .Include(u => u.ProfilePicture)
                .Include(u => u.Document)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (user is null) throw new Exception($"Cannot find user ID: {userId}");

            if (fileId == user.ProfilePicture?.Name)
            {
                await _fileUploadService.DeleteFileByNameAsync(user.ProfilePicture.Name);
                user.ProfilePicture = null;
            }
            else if (fileId == user.Document?.Name)
            {
                await _fileUploadService.DeleteFileByNameAsync(user.Document.Name);
                user.Document = null;
            }
            else
            {
                throw new Exception("Cannot delete or find file");
            }

            await _context.SaveChangesAsync();
        }

        public async Task<CountedListDto<UserForVerificationDto>> GetNotVerifiedUsersAsync(PaginatorDto input)
        {
            var students = await _context.Students
                .Include(s => s.ProfilePicture)
                .Include(s => s.Document)
                .ToListAsync();

            var owners = await _context.Owners
                .Include(s => s.ProfilePicture)
                .Include(s => s.Document)
                .ToListAsync();

            var studentDtos = _mapper.Map<List<UserForVerificationDto>>(students);
            var ownerDtos = _mapper.Map<List<UserForVerificationDto>>(owners);

            var users = new List<UserForVerificationDto>();

            users.AddRange(studentDtos);
            users.AddRange(ownerDtos);

            var totalCount = users.Count();

            users = users
                .OrderBy(user =>
                     user.VerificationStatus == VerificationStatus.NotVerified ? 0 :
                     user.VerificationStatus == VerificationStatus.Rejected ? 1 :
                     user.VerificationStatus == VerificationStatus.Verified ? 2 : 3)
                .ThenBy(user =>
                    user.VerificationStatus == VerificationStatus.NotVerified ? user.DateForVerificationSorting :
                    user.VerificationOrRejectionDate)
                .Skip((input.PageNumber - 1) * input.PageSize)
                .Take(input.PageSize)
                .ToList();

            var result = new CountedListDto<UserForVerificationDto>(users, totalCount);

            return result;
        }

        public async Task VerifyUserAsync(int id, bool decision)
        {
            var user = await _context.OwnerStudents
                .Include(u => u.Document)
                .FirstOrDefaultAsync(x => x.UserId == id);

            if (user is null) throw new ArgumentException($"User with ID {id} not found.");

            if (user.VerificationStatus == VerificationStatus.Verified) throw new ArgumentException($"User with ID {id} is already verified.");

            if (decision)
            {
                user.VerificationStatus = VerificationStatus.Verified;
                user.VerificationOrRejectionDate = DateTime.Now;
                user.DateForVerificationSorting = null;

                await _fileUploadService.DeleteFileByNameAsync(user.Document.Name);
                user.Document = null;
            }
            else
            {
                user.VerificationStatus = VerificationStatus.Rejected;
                user.VerificationOrRejectionDate = DateTime.Now;
                user.DateForVerificationSorting = null;
            }
            
            await _context.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(string oldPassword, string newPassword, int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.PasswordHash)) throw new AuthenticationException("Invalid old password.");

            if (newPassword.Length < 8 || newPassword.Length > 50) throw new Exception("Password must be between 8 and 50 characters");

            if (!newPassword.Any(char.IsUpper) || !newPassword.Any(char.IsLower) || !newPassword.Any(char.IsDigit)) throw new Exception("Password must contain at least one uppercase letter, one lowercase letter, and one digit");

            if (oldPassword.Equals(newPassword)) throw new Exception("Passwords must be different");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

            await _context.SaveChangesAsync();

            // Notify the user of password change
            var notificationTitle = "Password Changed";
            var notificationBody = "Your password has been successfully changed.";
            await _notificationService.SendNotificationAsync(notificationTitle, notificationBody, userId);

        }

        public async Task<UserProfileFullDto> GetCurrentUserProfileAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user is null) throw new ArgumentException($"User with ID {userId} not found.");

            UserProfileFullDto result;

            switch (user)
            {
                case Student:
                    var student = await _context.Students
                        .Include(s => s.ProfilePicture)
                        .Include(s => s.Document)
                        .Include(s => s.SurveyStudent)
                        .Include(s => s.Interests)
                        .Include(s => s.ReceivedUserOpinions)
                            .ThenInclude(ruo => ruo.SourceUser)
                                .ThenInclude(su => su.ProfilePicture)
                        .FirstOrDefaultAsync(o => o.UserId == userId);
                    result = _mapper.Map<UserProfileFullDto>(student);
                    break;
                case Owner:
                    var owner = await _context.Owners
                        .Include(s => s.ProfilePicture)
                        .Include(s => s.Document)
                        .Include(s => s.ReceivedUserOpinions)
                            .ThenInclude(ruo => ruo.SourceUser)
                                .ThenInclude(su => su.ProfilePicture)
                        .FirstOrDefaultAsync(o => o.UserId == userId);
                    result = _mapper.Map<UserProfileFullDto>(owner);
                    break;
                case Moderator:
                    var moderator = await _context.Moderators
                        .FirstOrDefaultAsync(o => o.UserId == userId);
                    result = _mapper.Map<UserProfileFullDto>(moderator);
                    break;
                default:
                    throw new ArgumentException($"Cannot get profile of this user");
            }

            return result;
        }

        public async Task<UserProfilePublicDto> GetUserProfileByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user is null) throw new ArgumentException($"User with ID {userId} not found.");

            UserProfilePublicDto result;

            switch (user)
            {
                case Student:
                    var student = await _context.Students
                        .Include(s => s.ProfilePicture)
                        .Include(s => s.SurveyStudent)
                        .Include(s => s.Interests)
                        .Include(s => s.ReceivedUserOpinions)
                            .ThenInclude(ruo => ruo.SourceUser)
                                .ThenInclude(su => su.ProfilePicture)
                        .FirstOrDefaultAsync(o => o.UserId == userId);
                    result = _mapper.Map<UserProfilePublicDto>(student);
                    break;
                case Owner:
                    var owner = await _context.Owners
                        .Include(s => s.ProfilePicture)
                        .Include(s => s.ReceivedUserOpinions)
                            .ThenInclude(ruo => ruo.SourceUser)
                                .ThenInclude(su => su.ProfilePicture)
                        .FirstOrDefaultAsync(o => o.UserId == userId);
                    result = _mapper.Map<UserProfilePublicDto>(owner);
                    break;
                default:
                    throw new ArgumentException($"Cannot get profile of this user");
            }

            return result;
        }

        public async Task<bool> CheckIfUserExistsByIdAsync(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

            return user != null;
        }

        public async Task SendPasswordResetLinkAsync(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

            if (user != null)
            {
                user.PasswordResetToken = Guid.NewGuid().ToString();
                user.PasswordResetTokenExpireDate = DateTime.Now.AddMinutes(30);

                await _context.SaveChangesAsync();

                var body = new StringBuilder();

                var link = "http://172.21.40.120/auth/reset-password?token=" + user.PasswordResetToken;

                body.AppendLine(EmailHelper.HtmlHTag("Dla twojego konta pojawiło się żądanie zmiany hasła", 1))
                    .AppendLine(EmailHelper.HtmlPTag($"Aby przejść do zmiany hasła naciśnij {EmailHelper.AddLinkToText(link, "TUTAJ")} lub przejdź pod poniższy link"))
                    .AppendLine(EmailHelper.HtmlPTag($"{link}"))
                    .AppendLine(EmailHelper.HtmlPTag("Jeżeli to nie ty prosiłeś o zmiane hasła wystarczy, że zignorujesz tę wiadomość"));

                await _emailService.SendEmailAsync(user.UserId, "Zmiana hasła w serwisie Flats4us", body.ToString());
            }
        }

        public async Task ResetUserPasswordAsync(string newPassword, string passwordResetToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.PasswordResetToken == passwordResetToken);

            if (user == null) throw new ArgumentException($"Token not found");

            if (user.PasswordResetTokenExpireDate < DateTime.Now) throw new ArgumentException($"Token expired");

            if (newPassword.Length < 8 || newPassword.Length > 50) throw new Exception("Password must be between 8 and 50 characters");

            if (!newPassword.Any(char.IsUpper) || !newPassword.Any(char.IsLower) || !newPassword.Any(char.IsDigit)) throw new Exception("Password must contain at least one uppercase letter, one lowercase letter, and one digit");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpireDate = null;

            await _context.SaveChangesAsync();
        }

        public async Task AddUserOpinionAsync(AddUserOpinionDto input, int targetUserId, int requestUserId)
        {
            var sourceUser = await _context.OwnerStudents.Include(s => s.IssuedUserOpinions)
                .FirstOrDefaultAsync(x => x.UserId == requestUserId);

            if (sourceUser is null) throw new ArgumentException($"User with ID {requestUserId} not found.");

            if (sourceUser.IssuedUserOpinions.Any(op => op.TargetUserId == targetUserId)) throw new Exception($"User {requestUserId} already added opinion for user {targetUserId}");

            var targetUser = await _context.Users.FindAsync(targetUserId);

            if (targetUser is null) throw new ArgumentException($"User with ID {targetUserId} not found.");

            if (sourceUser is Owner && targetUser is Owner) throw new ArgumentException("As an owner you cannot add opinion about another owner.");

            var opinion = new UserOpinion
            {
                Date = DateTime.Now,
                Rating = input.Rating,
                Helpful = input.Helpful,
                Cooperative = input.Cooperative,
                Tidy = input.Tidy,
                Friendly = input.Friendly,
                RespectingPrivacy = input.RespectingPrivacy,
                Communicative = input.Communicative,
                Unfair = input.Unfair,
                LackOfHygiene = input.LackOfHygiene,
                Untidy = input.Untidy,
                Conflicting = input.Conflicting,
                Noisy = input.Noisy,
                NotFollowingTheArrangements = input.NotFollowingTheArrangements,
                Description = input.Description,
                SourceUserId = sourceUser.UserId,
                TargetUserId = targetUser.UserId
            };

            await _context.UserOpinions.AddAsync(opinion);
            await _context.SaveChangesAsync();
        }

        public async Task<UserInfoDto> GetUserInfo(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user is null) throw new ArgumentException($"User with ID {userId} not found.");

            return _mapper.Map<UserInfoDto>(user);
        } 

        private TokenDto CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.GetType().Name),
                new Claim("VerificationStatus", user.VerificationStatus.ToString())
            };

            var key = new SymmetricSecurityKey(Convert.FromBase64String(_configuration.GetSection("Jwt:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var expiresAt = DateTime.Now.AddDays(1);
            DateTimeOffset dateTimeOffset = new DateTimeOffset(expiresAt);
            long unixTimestamp = dateTimeOffset.ToUnixTimeSeconds();
            
            var token = new JwtSecurityToken(
                claims: claims,
                expires: expiresAt,
                signingCredentials: creds
                );

            var result = new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = unixTimestamp,
                Role = user.GetType().Name,
                VerificationStatus = user.VerificationStatus
            };

            return result;
        }

        public async Task EditUserGeneral(EditUserGeneral input, int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            user.Address = input.Address ?? user.Address;
            user.Email = input.Email ?? user.Email;
            user.PhoneNumber = input.PhoneNumber ?? user.PhoneNumber;

            await _context.SaveChangesAsync();

        }

        public async Task EditUserSensitive(EditUserSensitive input, int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            user.Name = input.Name ?? user.Name;
            user.Surname = input.Surname ?? user.Name;
            user.VerificationStatus = VerificationStatus.NotVerified;

            await _context.SaveChangesAsync();
        }
        public async Task EditOwnerSensitive(EditOwnerSensitiveDto input, int userId)
        {
            var user = await _context.Owners.FindAsync(userId);
            user.DocumentNumber = input.DocumentNumber ?? user.DocumentNumber;
            user.DocumentExpireDate = input.DocumentExpireDate ?? user.DocumentExpireDate;
            user.VerificationStatus = VerificationStatus.NotVerified;
            await _context.SaveChangesAsync();
        }
        public async Task EditStudentSensitive(EditStudentSensitiveDto input, int userId)
        {

            var student = await _context.Students.FindAsync(userId);

            if (student == null)
            {
                throw new Exception($"Cannot find student with ID: {userId}");
            }

            student.BirthDate = input.BirthDate ?? student.BirthDate;
            student.StudentNumber = input.StudentNumber ?? student.StudentNumber;
            student.University = input.University ?? student.University;
            student.VerificationStatus = VerificationStatus.NotVerified;

            await _context.SaveChangesAsync();
        }

        public async Task EditUser(EditUserDto input, int userId)
        {
            bool isSensitiveDataUpdated = false;

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception($"User with ID {userId} not found.");
            }

            // Update general user data
            user.Address = input.Address ?? user.Address;
            user.Email = input.Email ?? user.Email;
            user.PhoneNumber = input.PhoneNumber ?? user.PhoneNumber;

            // Update sensitive user data
            if (input.Name != null || input.Surname != null)
            {
                isSensitiveDataUpdated = true;
                user.Name = input.Name ?? user.Name;
                user.Surname = input.Surname ?? user.Surname;
            }

            // Update Student-specific data
            if (user is Student student)
            {

                if (input.Links != null)
                {
                    student.Links = string.Join("|", input.Links);
                }

                if (input.BirthDate != null || input.StudentNumber != null || input.University != null)
                {
                    isSensitiveDataUpdated = true;
                    student.BirthDate = input.BirthDate ?? student.BirthDate;
                    student.StudentNumber = input.StudentNumber ?? student.StudentNumber;
                    student.University = input.University ?? student.University;
                }
            }
            // Update Owner-specific data
            else if (user is Owner owner)
            {
                owner.BankAccount = input.BankAccount ?? owner.BankAccount;

                if (input.DocumentNumber != null)
                {
                    isSensitiveDataUpdated = true;
                    owner.DocumentNumber = input.DocumentNumber;
                }
                // Additional fields for Owner can be updated here
            }

            // Set verification status if sensitive data is updated
            if (isSensitiveDataUpdated)
            {
                user.VerificationStatus = VerificationStatus.NotVerified;
            }

            await _context.SaveChangesAsync();
        }
    }
}
