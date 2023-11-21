using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class AddOfferPromotionDto
    {
        [Required]
        public int OfferId { get; set; }

        [Required]
        public int Duration { get; set; }
    }
}
