using Flats4us.Entities;

namespace Flats4us.Services
{
    public interface IOfferService
    {
        Task<IList<Offer>> GetOffersAsync();
        Task<IList<Offer>> GetOffersFilteredAsync(int maxPerPage, int skip, int minPrice, int maxPrice, int minArea, int maxArea);
    }
}
