using AutoMapper;
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

        public async Task<string> AuthenticateAsync(string email, string passwordHash)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(passwordHash, user.PasswordHash))
            {
                throw new AuthenticationException("Login failed: Invalid username or password.");
            }

            var token = CreateToken(user);

            return token;
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
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), // Add the user ID claim
                new Claim(ClaimTypes.Role, user.GetType().Name),
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
