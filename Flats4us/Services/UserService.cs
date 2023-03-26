using Flats4us.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class UserService : IUserService
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


    }
}
