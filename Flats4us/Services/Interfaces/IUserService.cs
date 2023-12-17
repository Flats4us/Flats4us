using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> AuthenticateAsync(string email, string password);
        Task<int> RegisterOwnerAsync(OwnerRegisterDto input);
        Task<int> RegisterStudentAsync(StudentRegisterDto input);
        Task RegisterUserFilesAsync(UserRegisterFilesDto input, int userId);
        Task<CountedListDto<UserForVerificationDto>> GetNotVerifiedUsersAsync(PaginatorDto input);
        Task VerifyUserAsync(int id, bool decision);
        Task ChangePasswordAsync(string oldPassword, string newPassword, int userId);
    }
}
