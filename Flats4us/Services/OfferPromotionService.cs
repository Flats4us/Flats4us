using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class OfferPromotionService : IOfferPromotionService
    {
        public readonly Flats4usContext _context;

        public OfferPromotionService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task AddOfferPromotionAsync(AddOfferPromotionDto input)
        {
            var offer = await _context.Offers.FindAsync(input.OfferId);

            if (offer is null) throw new ArgumentException($"Offer with ID {input.OfferId} not found.");

            var offerPromotion = new OfferPromotion
            {
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date.AddDays(input.Duration),
                Price = input.Duration * OfferPromotion.PricePerDay,
            };

            offer.OfferPromotions.Add(offerPromotion);
            await _context.SaveChangesAsync();
        }
    }
}
