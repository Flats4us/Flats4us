using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IOfferService
    {
        Task<OfferListDto> GetAllAsync();
        Task<OfferDto> GetByIdAsync(int id);
        Task<OfferListDto> GetFilteredAndSortedOffersAsync(GetFilteredAndSortedOffersDto input);
        Task AddOfferAsync(AddEditOfferDto input, int ownerId);
        Task AddOfferPromotionAsync(AddOfferPromotionDto input, int userId);
        Task<List<OfferDto>> GetOffersByInterestAsync(int studentId);
        Task AddOfferInterestAsync(int offerId, int studentId);
        Task RemoveOfferInterestAsync(int offerId, int studentId);
    }
}