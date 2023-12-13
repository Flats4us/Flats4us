using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class GetMeetingsDto
    {
        [Required]
        [Range(1, 12)]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }
    }
}
