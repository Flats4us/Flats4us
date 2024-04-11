using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<List<PropertyDto>> GetPropertiesForCurrentUserAsync(int ownerId, bool showOnlyVerified);
        Task<CountedListDto<PropertyForVerificationDto>> GetNotVerifiedPropertiesAsync(PaginatorDto input);
        Task<OutputDto<int>> AddPropertyAsync(AddEditPropertyDto input, int requestUserId);
        Task AddPropertyFilesAsync(PropertyFilesDto input, int propertyId, int requestUserId);
        Task DeletePropertyFileAsync(int propertyId, string fileId, int requestUserId);
        Task UpdatePropertyAsync(int id, AddEditPropertyDto input, int requestUserId);
        Task DeletePropertyAsync(int id, int requestUserId);
        Task VerifyPropertyAsync(int id, bool decision);
        //Task<PropertyWithRentOpinionDto> GetPropertyByIdAsync(int PropertyId);
    }
}
