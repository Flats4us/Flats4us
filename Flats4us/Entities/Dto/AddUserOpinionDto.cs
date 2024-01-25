using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class AddUserOpinionDto
    {
        [Required]
        [Range(1, 10)]
        public int Rating { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
