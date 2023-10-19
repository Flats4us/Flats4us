using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<List<PropertyDto>> GetNotVerifiedPropertiesAsync();
        Task AddPropertyAsync(NewPropertyDto tenant);
        Task DeletePropertyAsync(int id);
    }
}
