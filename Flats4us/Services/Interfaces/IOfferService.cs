using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IOfferService
    {
        Task<List<OfferDto>> GetAllAsync();
        Task<OfferDto> GetByIdAsync(int id);
        Task<List<OfferDto>> GetFilteredAndSortedOffersAsync(GetFilteredAndSortedOffersDto input);
        Task AddOfferAsync(AddEditOfferDto input);
        Task AddOfferPromotionAsync(AddOfferPromotionDto input);
    }
}