using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class OfferPromotion
    {
        [ForeignKey("Offer")]
        public int OfferPromotionId { get; set; }

        // In Days
        [Required]
        public int Duration { get; set; }

        [Required]
        public int PricePerDay { get; set; }

        public virtual Offer Offer { get; set; }
    }
}
