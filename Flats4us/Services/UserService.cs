﻿using AutoMapper;
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

        public async Task<OutputDto<int>> RegisterOwnerAsync(OwnerRegisterDto input)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(x => x.Email == input.Email);

            if (existingUser != null) throw new Exception("Email already exists");

            if (input.Password.Length < 8 || input.Password.Length > 50) throw new Exception("Password must be between 8 and 50 characters");

            if (!input.Password.Any(char.IsUpper) || !input.Password.Any(char.IsLower) || !input.Password.Any(char.IsDigit)) throw new Exception("Password must contain at least one uppercase letter, one lowercase letter, and one digit");
            
            var owner = _mapper.Map<Owner>(input);

            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();

            var result = new OutputDto<int>(owner.UserId);

            return result;
        }

        public async Task<OutputDto<int>> RegisterStudentAsync(StudentRegisterDto input)
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

            var result = new OutputDto<int>(student.UserId);

            return result;
        }

        public async Task RegisterUserFilesAsync(UserRegisterFilesDto input, int userId)
        {
            var user = await _context.OwnerStudents.FindAsync(userId);

            if (user is null) throw new Exception($"Cannot find user ID: {userId}");

            if (user.VerificationStatus != VerificationStatus.PreCreated) throw new Exception("Files already uploaded");

            var directoryPath = Path.Combine("Images/Users", user.ImagesPath);

            if (input.ProfilePicture != null && input.ProfilePicture.Length > 0)
            {
                await ImageUtility.ProcessAndSaveImage(input.ProfilePicture, Path.Combine(directoryPath, "ProfilePicture"));
            }
            else
            {
                _context.Users.Remove(user);
                await ImageUtility.DeleteDirectory(directoryPath);
                throw new Exception("Profile picture saving failure");
            }

            if (input.Document != null && input.Document.Length > 0)
            {
                await ImageUtility.ProcessAndSaveImage(input.Document, Path.Combine(directoryPath, "Documents"));
            }
            else
            {
                _context.Users.Remove(user);
                await ImageUtility.DeleteDirectory(directoryPath);
                throw new Exception("Document saving failure");
            }

            user.VerificationStatus = VerificationStatus.NotVerified;
            await _context.SaveChangesAsync();
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

            var result = new CountedListDto<UserForVerificationDto>
            {
                TotalCount = totalCount,
                Result = users
            };

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

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expiresAt,
                signingCredentials: creds
                );

            var result = new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = expiresAt
            };

            return result;
        }
    }
}
