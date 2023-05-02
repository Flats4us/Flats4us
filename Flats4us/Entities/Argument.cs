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

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advertisement>().HasData(
            new Argument
            {
                Id = 1,
                StartDate = new DateTime(2023, 05, 01),
                OwnerAcceptanceDate = new DateTime(2023, 05, 02),
                TenantAcceptanceDate = new DateTime(2023, 05, 03),
                ArgumentStatus = ArgumentStatus.Ongoing,
                ModeratorDecisionDate = new DateTime(2023, 05, 04)
            },
            new Argument
            {
                Id = 2,
                StartDate = new DateTime(2023, 05, 02),
                OwnerAcceptanceDate = new DateTime(2023, 05, 03),
                TenantAcceptanceDate = new DateTime(2023, 05, 04),
                ArgumentStatus = ArgumentStatus.Resolved,
                ModeratorDecisionDate = new DateTime(2023, 05, 05)
            },
            new Argument
            {
                Id = 3,
                StartDate = new DateTime(2023, 05, 03),
                OwnerAcceptanceDate = new DateTime(2023, 05, 04),
                TenantAcceptanceDate = new DateTime(2023, 05, 05),
                ArgumentStatus = ArgumentStatus.ResolvedByMod,
                ModeratorDecisionDate = new DateTime(2023, 05, 06)
            }); ;

        }
    }
}
