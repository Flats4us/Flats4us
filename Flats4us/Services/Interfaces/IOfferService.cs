using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IOfferService
    {
        Task<OfferDto> GetByIdAsync(int id, int requestUserId);
        Task<CountedListDto<OfferDto>> GetFilteredAndSortedOffersAsync(GetFilteredAndSortedOffersDto input, int requestUserId);
        Task<CountedListDto<SimpleOfferForMapDto>> GetFilteredOffersForMapAsync(GetFilteredOffersDto input);
        Task<CountedListDto<OfferDto>> GetOffersForCurrentUserAsync(int ownerId);
        Task AddOfferAsync(AddEditOfferDto input, int ownerId);
        Task DeleteOfferAsync(int id, int requestUserId);
        Task AddOfferPromotionAsync(int duration, int offerId, int userId);
        Task<CountedListDto<OfferDto>> GetOffersByInterestAsync(PaginatorDto input, int studentId);
        Task AddOfferInterestAsync(int offerId, int studentId);
        Task RemoveOfferInterestAsync(int offerId, int studentId);
    }
}