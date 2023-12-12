namespace Flats4us.Entities.Dto
{
    public class OfferListDto
    {
        public int TotalNumberOfOffers { get; set; }

        public ICollection<OfferDto> PromotedOffers { get; set; }

        public ICollection<OfferDto> Offers { get; set; }
    }
}
