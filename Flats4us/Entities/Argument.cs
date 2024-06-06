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
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? OwnerAcceptanceDate { get; set; }

        public DateTime? StudentAccceptanceDate { get; set; }

        [Required]
        public ArgumentStatus ArgumentStatus { get; set; }

        public bool InterventionNeed { get; set; }

        public DateTime? InterventionNeedDate { get; set; }

        public DateTime? MederatorDecisionDate { get; set; }

        [Required]
        public int RentId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int GroupChatId { get; set; }

        public virtual Rent Rent { get; set; }
        public virtual Student Student { get; set; }
        public virtual GroupChat GroupChat { get; set; }

        public virtual ICollection<ArgumentIntervention> ArgumentInterventions { get; set; }

        public Argument()
        {
            this.ArgumentInterventions = new HashSet<ArgumentIntervention>();
        }
    }
}
