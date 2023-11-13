using Flats4us.Entities.Dto;
using Flats4us.Entities;

namespace Flats4us.Services.Interfaces
{
    public interface IOwnerService
    {
        Task<User?> GetUserByIdAsync(int userId);
        Task<User> RegisterAsync(UserRegisterDto userDto);
    }
}
