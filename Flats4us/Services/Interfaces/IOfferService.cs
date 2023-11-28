using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IOfferService
    {
        Task<OfferListDto> GetAllAsync();
        Task<OfferDto> GetByIdAsync(int id);
        Task<OfferListDto> GetFilteredAndSortedOffersAsync(GetFilteredAndSortedOffersDto input);
        Task AddOfferAsync(AddEditOfferDto input);
        Task AddOfferPromotionAsync(AddOfferPromotionDto input, int userId);
        Task AddOfferInterest(int offerId, int studentId);
        Task RemoveOfferInterest(int offerId, int studentId);
    }
}