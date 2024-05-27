using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class RentOpinionDto
    {
        [Required]
        public int RentOpinionId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(1, 10)]
        public int Rating { get; set; }

        public string? Description { get; set; }

        [Required]
        public string SourceUserName { get; set; }

        [Required]
        public FileDto SourceUserProfilePicture { get; set; }
    }
}
