using Flats4us.Entities;

namespace Flats4us.Services
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string passwordHash);

    }
}
