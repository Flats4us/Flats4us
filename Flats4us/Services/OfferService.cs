using Flats4us.Entities;

namespace Flats4us.Services
{
    public class OfferService : IOfferService
    {
        public readonly Flats4usContext _context;
        public OfferService(Flats4usContext context) 
        {
            _context = context;
        }   
        public Task<Offer> GetOffer(int skipCount, int maxResoultCount, string wojewodzto, string miejscowosc, int odleglosc, int cenaMin, int cenaMax, string dzielnica, int powierzchniaOd, int powierzchniaDo, int LataBudowy)
        {
            //filtr = _context.Offers.Where(x =>x.OfferId == id).Select(x=>x.).FirstOrDefault();


            //return filtr;
            return null;
        }
    }
}
