using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class AddArgumentInterventionDto
    {
        [Required]
        public string Justification { get; set; }

        [Required]
        public int ArgumentId { get; set; }
    }
}
