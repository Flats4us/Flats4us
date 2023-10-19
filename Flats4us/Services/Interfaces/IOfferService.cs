using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IOfferService
    {
        Task<List<OfferDto>> GetAll();

        Task<List<OfferDto>> GetFilteredAndSortedOffers(GetFilteredAndSortedOffersDto input);
        
    }
}