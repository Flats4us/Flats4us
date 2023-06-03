using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Flats4us.Helpers.Enums;

namespace Flats4us.Entities
{
    public class Argument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArgumentId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime StudentAcceptanceDate { get; set; }

        public DateTime OwnerAcceptanceDate { get; set; }

        [Required]
        public ArgumentStatus ArgumentStatus { get; set; }

        public DateTime MederatorDecisionDate { get; set; }

        public virtual Offer Offer { get; set; }

        public virtual Student Student { get; set; }

        public virtual ArgumentIntervention? ArgumentIntervention { get; set; }

        public virtual ICollection<ArgumentMessage> ArgumentMessages { get; set; }

        public Argument()
        {
            this.ArgumentMessages= new HashSet<ArgumentMessage>();
        }
    }
}
