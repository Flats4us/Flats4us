using Flats4us.Migrations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public enum ArgumentStatus
    {
        Ongoing,
        Resolved,
        Unfounded,
        ResolvedByMod,
        UnfoundedByMod
    }

    [Table("Argument")]
    public class Argument          //not abstract
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime OwnerAcceptanceDate { get; set; }

        public DateTime TenantAcceptanceDate { get; set; }

        [Required]
        public ArgumentStatus ArgumentStatus { get; set; }

        public DateTime ModeratorDecisionDate { get; set; }

        public virtual Offer Offer { get; set; }

        public virtual Intervention? Intervention { get; set; }

        public virtual ICollection<ArgumentMessage> ArgumentMessages { get; set; }

        public Argument()
        {
            ArgumentMessages = new HashSet<ArgumentMessage>();
        }


    }
}
