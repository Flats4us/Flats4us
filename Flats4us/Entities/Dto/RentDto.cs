using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class RentDto
    {
        [Required]
        public int RentId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
