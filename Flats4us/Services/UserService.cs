using AutoMapper;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers;
using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class UserService : IUserService
    {
        public readonly Flats4usContext _context;
        private readonly IMapper _mapper;

        public UserService(Flats4usContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> AuthenticateAsync(string email, string passwordHash)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(passwordHash, user.PasswordHash))
            {
                return null;
            }

            return user;
        }

        protected User PopulateCommonFieldsFromDto(User user, UserRegisterDto request)
        {
            user.Username = request.Username;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.AccountCreationDate = DateTime.UtcNow;
            user.LastLoginDate = DateTime.UtcNow;
            user.Name = request.Name;
            user.Surname = request.Surname;
            user.Street = request.Street;
            user.Number = request.Number;
            user.Flat = request.Flat;
            user.City = request.City;
            user.PostalCode = request.PostalCode;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            // ... any other common fields ...

            return user;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }

        async Task<IEnumerable<User>> IUserService.GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        async Task<bool> IUserService.DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
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
    }
}
