using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IOfferService
    {
        Task<OfferDto> GetByIdAsync(int id);
        Task<CountedListDto<OfferDto>> GetFilteredAndSortedOffersAsync(GetFilteredAndSortedOffersDto input);
        Task AddOfferAsync(AddEditOfferDto input, int ownerId);
        Task AddOfferPromotionAsync(int duration, int offerId, int userId);
        Task<CountedListDto<OfferDto>> GetOffersByInterestAsync(PaginatorDto input, int studentId);
        Task AddOfferInterestAsync(int offerId, int studentId);
        Task RemoveOfferInterestAsync(int offerId, int studentId);
    }
}