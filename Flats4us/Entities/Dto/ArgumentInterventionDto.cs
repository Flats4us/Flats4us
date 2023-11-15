using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class ArgumentInterventionDto
    {
        public int ArgumentInterventionId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Justification { get; set; }
    }
}
