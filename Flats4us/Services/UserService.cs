using AutoMapper;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers;
using Flats4us.Helpers.Enums;
using Flats4us.Helpers.Exceptions;
using Flats4us.Services.Interfaces;
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
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(Flats4usContext context,
            IMapper mapper,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<TokenDto> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) throw new AuthenticationException("Invalid email or password.");

            user.ActivityStatus = true;
            user.LastLoginDate = DateTime.Now;

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
                .Where(i => input.Interests
                    .Select(i => i.InterestId)
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
            var user = await _context.OwnerStudents.FindAsync(userId);

            if (user is null) throw new Exception($"Cannot find user ID: {userId}");

            await ImageUtility.SaveUserFilesAsync(user.ImagesPath, input);
        }

        public async Task DeleteUserFileAsync(string fileId, int userId)
        {
            try
            {
                var ownerStudent = await _context.OwnerStudents.FindAsync(userId);

                if (ownerStudent is null) throw new Exception($"Cannot find user ID: {userId}");

                await ImageUtility.DeleteUserFileAsync(ownerStudent.ImagesPath, fileId);
            }
            catch (IOException ex)
            {
                throw new IOException($"File operation failed: {ex.Message}");
            }
        }

        public async Task<CountedListDto<UserForVerificationDto>> GetNotVerifiedUsersAsync(PaginatorDto input)
        {
            var students = await _context.Students
                .Where(p => p.VerificationStatus == VerificationStatus.NotVerified)
                .ToListAsync();

            var owners = await _context.Owners
                .Where(p => p.VerificationStatus == VerificationStatus.NotVerified)
                .ToListAsync();

            var studentDtos = _mapper.Map<List<UserForVerificationDto>>(students);
            var ownerDtos = _mapper.Map<List<UserForVerificationDto>>(owners);

            var users = new List<UserForVerificationDto>();

            users.AddRange(studentDtos);
            users.AddRange(ownerDtos);

            var totalCount = users.Count();

            users = users
                .OrderBy(user => user.DateForVerificationSorting)
                .Skip((input.PageNumber - 1) * input.PageSize)
                .Take(input.PageSize)
                .ToList();

            var result = new CountedListDto<UserForVerificationDto>(users, totalCount);

            return result;
        }

        public async Task VerifyUserAsync(int id, bool decision)
        {
            var user = await _context.OwnerStudents.FindAsync(id);

            if (user is null) throw new ArgumentException($"User with ID {id} not found.");

            if (user.VerificationStatus == VerificationStatus.Verified) throw new ArgumentException($"User with ID {id} is already verified.");

            if (decision)
            {
                user.VerificationStatus = VerificationStatus.Verified;

                var documentDirectoryPath = Path.Combine("Images/Users", user.ImagesPath, "Documents");

                await ImageUtility.DeleteDirectory(documentDirectoryPath);
            }
            else
            {
                user.VerificationStatus = VerificationStatus.Rejected;
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
                        .Include(s => s.SurveyStudent)
                        .Include(s => s.Interests)
                        .Include(s => s.ReceivedUserOpinions)
                            .ThenInclude(ruo => ruo.SourceUser)
                        .FirstOrDefaultAsync(o => o.UserId == userId);
                    result = _mapper.Map<UserProfileFullDto>(student);
                    break;
                case Owner:
                    var owner = await _context.Owners
                        .Include(s => s.ReceivedUserOpinions)
                            .ThenInclude(ruo => ruo.SourceUser)
                        .FirstOrDefaultAsync(o => o.UserId == userId);
                    result = _mapper.Map<UserProfileFullDto>(owner);
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
                        .Include(s => s.SurveyStudent)
                        .Include(s => s.Interests)
                        .Include(s => s.ReceivedUserOpinions)
                            .ThenInclude(ruo => ruo.SourceUser)
                        .FirstOrDefaultAsync(o => o.UserId == userId);
                    result = _mapper.Map<UserProfilePublicDto>(student);
                    break;
                case Owner:
                    var owner = await _context.Owners
                        .Include(s => s.ReceivedUserOpinions)
                            .ThenInclude(ruo => ruo.SourceUser)
                        .FirstOrDefaultAsync(o => o.UserId == userId);
                    result = _mapper.Map<UserProfilePublicDto>(owner);
                    break;
                default:
                    throw new ArgumentException($"Cannot get profile of this user");
            }

            return result;
        }

        public async Task<bool> CheckIfStudentExistsByIdAsync(string email)
        {
            var student = await _context.Students.SingleOrDefaultAsync(x => x.Email == email);

            return student != null;
        }

        public async Task AddUserOpinionAsync(AddUserOpinionDto input, int targetUserId, int requestUserId)
        {
            var sourceUser = await _context.Users.FindAsync(requestUserId);

            if (sourceUser is null) throw new ArgumentException($"User with ID {requestUserId} not found.");

            var targetUser = await _context.Users.FindAsync(targetUserId);

            if (targetUser is null) throw new ArgumentException($"User with ID {targetUserId} not found.");

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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Key").Value!));

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
                Role = user.GetType().Name
            };

            return result;
        }
    }
}
