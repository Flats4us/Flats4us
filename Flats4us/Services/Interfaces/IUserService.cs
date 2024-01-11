﻿using Flats4us.Entities;
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
        Task EditUserGeneral(EditUserGeneral input, int userId);
        Task EditUserSensitive(EditUserSensitive input, int userId);
        Task EditOwnerSensitive(EditOwnerSensitiveDto input, int userId);
        Task EditStudentSensitive(EditStudentSensitiveDto input, int userId);

    }
}
