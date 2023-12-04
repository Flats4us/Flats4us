using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<List<PropertyDto>> GetPropertiesForCurrentUserAsync(int ownerId);
        Task<List<PropertyForVerificationDto>> GetNotVerifiedPropertiesAsync();
        Task AddPropertyAsync(AddEditPropertyDto input, int requestUserId);
        Task UpdatePropertyAsync(int id, AddEditPropertyDto input, int requestUserId);
        Task DeletePropertyAsync(int id, int requestUserId);
        Task VerifyPropertyAsync(int id, bool decision);
    }
}
