using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class OfferPromotion
    {
        public const int PricePerDay = 10;

        [ForeignKey("Offer")]
        public int OfferPromotionId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int Price { get; set; }

        public virtual Offer Offer { get; set; }
    }
}
