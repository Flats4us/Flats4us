namespace Flats4us.Entities.Dto
{
    public class OfferListDto
    {
        public ICollection<OfferDto> PromotedOffers { get; set; }

        public ICollection<OfferDto> Offers { get; set; }
    }
}
