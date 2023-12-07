using Flats4us.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class MeetingDto
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public int OfferId { get; set; }
    }
}
