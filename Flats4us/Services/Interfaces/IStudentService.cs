using Flats4us.Entities.Dto;
using Flats4us.Entities;

namespace Flats4us.Services.Interfaces
{
    public interface IStudentService
    {
        Task<User> RegisterAsync(UserRegisterDto userDto);
        Task<User> AuthenticateAsync(string username, string password);
        Task<User> GetUserByIdAsync(int userId);
    }
}
