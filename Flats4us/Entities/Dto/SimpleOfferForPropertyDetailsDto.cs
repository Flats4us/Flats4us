using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class SimpleOfferForPropertyDetailsDto
    {
        [Required]
        public int OfferId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public OfferStatus OfferStatus { get; set; }
    }
}
