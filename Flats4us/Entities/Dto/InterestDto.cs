using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class InterestDto
    {
        [Required]
        public int InterestId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
