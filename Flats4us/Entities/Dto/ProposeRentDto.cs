using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class ProposeRentDto
    {
        [Required]
        public List<string> RoommatesEmails { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        // In months
        [Required]
        public int Duration { get; set; }
    }
}
