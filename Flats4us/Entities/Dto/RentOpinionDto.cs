using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class RentOpinionDto
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

        [Required]
        public string Description { get; set; }

        [Required]
        public User UserId { get; set; }

        [Required]
        public Rent RentId { get; set; }
    }
}
