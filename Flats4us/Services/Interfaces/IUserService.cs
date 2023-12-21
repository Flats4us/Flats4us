using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IUserService
    {
        Task<TokenDto> AuthenticateAsync(string email, string password);
        Task<OutputDto<int>> RegisterOwnerAsync(OwnerRegisterDto input);
        Task<OutputDto<int>> RegisterStudentAsync(StudentRegisterDto input);
        Task RegisterUserFilesAsync(UserRegisterFilesDto input, int userId);
        Task<CountedListDto<UserForVerificationDto>> GetNotVerifiedUsersAsync(PaginatorDto input);
        Task VerifyUserAsync(int id, bool decision);
        Task ChangePasswordAsync(string oldPassword, string newPassword, int userId);
    }
}
