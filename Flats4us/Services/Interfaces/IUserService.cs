using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IUserService
    {
        Task<TokenDto> AuthenticateAsync(string email, string password);
        Task<TokenDto> RegisterOwnerAsync(OwnerRegisterDto input);
        Task<TokenDto> RegisterStudentAsync(StudentRegisterDto input);
        Task AddUserFilesAsync(UserFilesDto input, int userId);
        Task DeleteUserFileAsync(string fileId, int userId);
        Task<CountedListDto<UserForVerificationDto>> GetNotVerifiedUsersAsync(PaginatorDto input);
        Task VerifyUserAsync(int id, bool decision);
        Task ChangePasswordAsync(string oldPassword, string newPassword, int userId);
        Task<UserProfileFullDto> GetCurrentUserProfileAsync(int userId);
        Task<UserProfilePublicDto> GetUserProfileByIdAsync(int userId);
        Task<bool> CheckIfStudentExistsByIdAsync(string email);
        Task AddUserOpinionAsync(AddUserOpinionDto input, int targetUserId, int requestUserId);
        Task<UserInfoDto> GetUserInfo(int userId);
    }
}
