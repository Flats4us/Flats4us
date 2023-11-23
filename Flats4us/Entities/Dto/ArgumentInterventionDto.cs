using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class ArgumentInterventionDto
    {
        [ForeignKey("Argument")]
        public int ArgumentInterventionId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Justification { get; set; }

        [Required]
        public bool InterventionNeed { get; set; }

        [Required]
        public int ModeratorId { get; set; }
    }
}
