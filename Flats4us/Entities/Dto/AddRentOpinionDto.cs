using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class AddRentOpinionDto
    {
        [Required]
        [Range(1, 10)]
        public int Rating { get; set; }

        [Required]
        [Range(1, 10)]
        public int Service { get; set; }

        [Required]
        [Range(1, 10)]
        public int Location { get; set; }

        [Required]
        [Range(1, 10)]
        public int Equipment { get; set; }

        [Required]
        [Range(1, 10)]
        public int QualityForMoney { get; set; }

        public string? Description { get; set; }
    }
}
