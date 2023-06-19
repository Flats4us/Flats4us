using Flats4us.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class OfferService : IOfferService
    {
        public readonly Flats4usContext _context;

        public OfferService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task<IList<Offer>> GetOffersAsync()
        {
            return await _context.Offers.ToListAsync();
        }

        public async Task<IList<Offer>> GetOffersFilteredAsync(int maxPerPage, int skip, int minPrice, int maxPrice, int minArea, int maxArea)
        {
            var offers = await _context.Offers.Where(o =>
                o.Price >= minPrice &&
                o.Price <= maxPrice &&
                o.Property.Area >= minArea &&
                o.Property.Area <= maxArea)
            .Skip(skip)
            .Take(maxPerPage)
            .ToListAsync();;

            return offers;
        }
    }
}
