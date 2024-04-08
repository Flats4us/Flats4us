using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class SimpleOfferForMapDto
    {
        [Required]
        public int OfferId { get; set; }

        [Required]
        public bool IsPromoted { get; set; }

        [Required]
        public SimplePropertyDto Property { get; set; }
    }
}
