using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IInterestService
    {
        Task<List<InterestDto>> GetAll(string? name = null);
    }
}
