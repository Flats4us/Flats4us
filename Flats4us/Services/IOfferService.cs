using Flats4us.Entities;

namespace Flats4us.Services
{
    public interface IOfferService
    {
        public Task<Offer> GetOffer(int id, Property filtr);
    }
}
