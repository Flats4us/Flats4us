using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class ArgumentIntervention
    {
        [ForeignKey("Argument")]
        public int ArgumentInterventionId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Justification { get; set; }

        public virtual Moderator Moderator { get; set; }
    }
}
