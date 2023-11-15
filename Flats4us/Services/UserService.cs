using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public abstract class UserService : IUserService
    {
        public readonly Flats4usContext _context;

        public UserService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(string username, string passwordHash)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);

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
    }
}
