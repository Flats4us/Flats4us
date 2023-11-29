using Flats4us.Entities.Dto;
using Flats4us.Entities;

namespace Flats4us.Services.Interfaces
{
    public interface IStudentService
    {
        Task RegisterAsync(StudentRegisterDto userDto);
        Task<User?> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int id);
    }
}
