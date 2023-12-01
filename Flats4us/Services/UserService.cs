using AutoMapper;
using Azure.Core;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers;
using Flats4us.Helpers.Enums;
using Flats4us.Helpers.Exceptions;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                throw new AuthenticationException("Login failed: Invalid email or password.");
            }

            user.ActivityStatus = true;
            user.LastLoginDate = DateTime.Now;

            await _context.SaveChangesAsync();

            var token = CreateToken(user);

            return token;
        }

        public async Task RegisterOwnerAsync(OwnerRegisterDto input)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(x => x.Email == input.Email);
            if (existingUser != null)
            {
                throw new Exception("Email already exists");
            }

            if (input.Password.Length < 8 || input.Password.Length > 50)
            {
                throw new Exception("Password must be between 8 and 50 characters");
            }

            if (!input.Password.Any(char.IsUpper) || !input.Password.Any(char.IsLower) || !input.Password.Any(char.IsDigit))
            {
                throw new Exception("Password must contain at least one uppercase letter, one lowercase letter, and one digit");
            }

            var owner = _mapper.Map<Owner>(input);

            if (input.ProfilePicture != null && input.ProfilePicture.Length > 0)
            {
                await ImageUtility.ProcessAndSaveImage(input.ProfilePicture, $"Images/Users/{owner.ImagesPath}/ProfilePicture");
            }
            else
            {
                throw new Exception("Profile picture saving failure");
            }

            if (input.Document != null && input.Document.Length > 0)
            {
                await ImageUtility.ProcessAndSaveImage(input.Document, $"Images/Users/{owner.ImagesPath}/Document");
            }
            else
            {
                throw new Exception("Document saving failure");
            }

            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();
        }

        public async Task RegisterStudentAsync(StudentRegisterDto input)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(x => x.Email == input.Email);
            if (existingUser != null)
            {
                throw new Exception("Email already exists");
            }

            if (input.Password.Length < 8 || input.Password.Length > 50)
            {
                throw new Exception("Password must be between 8 and 50 characters");
            }

            if (!input.Password.Any(char.IsUpper) || !input.Password.Any(char.IsLower) || !input.Password.Any(char.IsDigit))
            {
                throw new Exception("Password must contain at least one uppercase letter, one lowercase letter, and one digit");
            }

            var student = _mapper.Map<Student>(input);

            var studentSurvey =_mapper.Map<SurveyStudent>(input);

            student.SurveyStudent = studentSurvey;

            if (input.ProfilePicture != null && input.ProfilePicture.Length > 0)
            {
                await ImageUtility.ProcessAndSaveImage(input.ProfilePicture, $"Images/Users/{student.ImagesPath}/ProfilePicture");
            }
            else
            {
                throw new Exception("Profile picture saving failure");
            }

            if (input.Document != null && input.Document.Length > 0)
            {
                await ImageUtility.ProcessAndSaveImage(input.Document, $"Images/Users/{student.ImagesPath}/Document");
            }
            else
            {
                throw new Exception("Document saving failure");
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        protected User PopulateCommonFieldsFromDto(User user, UserRegisterDto request)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.AccountCreationDate = DateTime.UtcNow;
            user.LastLoginDate = DateTime.UtcNow;
            user.Name = request.Name;
            user.Surname = request.Surname;
            user.Address = (request.Flat != null) ?
                $"{request.Street} {request.Number}/{request.Flat}, {request.PostalCode} {request.City}" :
                $"{request.Street} {request.Number}, {request.PostalCode} {request.City}";
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            // ... any other common fields ...

            return user;
        }

        public async Task<List<UserForVerificationDto>> GetNotVerifiedUsersAsync()
        {
            var students = await _context.Students
                .Where(p => p.VerificationStatus == VerificationStatus.NotVerified)
                .ToListAsync();

            var owners = await _context.Owners
                .Where(p => p.VerificationStatus == VerificationStatus.NotVerified)
                .ToListAsync();

            var studentDtos = _mapper.Map<List<UserForVerificationDto>>(students);
            var ownerDtos = _mapper.Map<List<UserForVerificationDto>>(owners);

            var result = new List<UserForVerificationDto>();
            result.AddRange(studentDtos);
            result.AddRange(ownerDtos);

            return result;
        }

        public async Task VerifyUserAsync(int id)
        {
            var user = await _context.OwnerStudents.FindAsync(id);

            if (user is null)
            {
                throw new ArgumentException($"User with ID {id} not found.");
            }

            if (user.VerificationStatus == VerificationStatus.Verified)
            {
                throw new ArgumentException($"User with ID {id} is already verified.");
            }

            user.VerificationStatus = VerificationStatus.Verified;

            var documentDirectoryPath = Path.Combine("Images/Users", user.ImagesPath, "Documents");

            await ImageUtility.DeleteDirectory(documentDirectoryPath);

            await _context.SaveChangesAsync();
        }

        private string CreateToken(User user)
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

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
