using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class AddMeetingDto
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public int OfferId { get; set; }

        [Required]
        public List<int> StudentIds { get; set; }
    }
}
