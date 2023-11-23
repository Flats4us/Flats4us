using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string email, string password);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<bool> DeleteUserAsync(int id);
        Task<List<UserForVerificationDto>> GetNotVerifiedUsersAsync();
        Task VerifyUserAsync(int id);
    }
}
