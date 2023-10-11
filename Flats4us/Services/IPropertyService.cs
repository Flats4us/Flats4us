using Flats4us.Entities.Dto;

namespace Flats4us.Services
{
    public interface IPropertyService
    {
        Task AddPropertyAsync(NewPropertyDto tenant);
        Task DeletePropertyAsync(int id);
    }
}
