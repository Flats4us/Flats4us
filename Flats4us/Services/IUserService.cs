using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User> RegisterAsync(UserRegisterDto userDto);
        //Task<User?> UpdateUserAsync(int id, UserUpdateDto userUpdateDto);
        Task<bool> DeleteUserAsync(int id);
    }

}
