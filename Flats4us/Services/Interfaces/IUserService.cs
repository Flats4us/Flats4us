using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> AuthenticateAsync(string email, string password);
        Task RegisterOwnerAsync(OwnerRegisterDto input);
        Task RegisterStudentAsync(StudentRegisterDto input);
        Task<List<UserForVerificationDto>> GetNotVerifiedUsersAsync();
        Task VerifyUserAsync(int id, bool decision);
    }
}
