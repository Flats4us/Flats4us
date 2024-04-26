using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class ArgumentDto
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public int RentId { get; set; }

        [Required]
        public bool InterventionNeed { get; set; }
    }
}
