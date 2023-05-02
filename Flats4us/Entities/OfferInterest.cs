using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("OfferInterest")]
    public class OfferInterest  //not abstract
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int OfferId { get; set; }

        [Required]
        public int SeekerId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual Offer Offer { get; set; }
        public virtual Seeker Seeker { get; set; }
        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OfferInterest>().HasData(
                new OfferInterest
                {
                    Id = 1,
                    OfferId = 1,
                    SeekerId = 1,
                    Date = DateTime.UtcNow.AddDays(-5)
                },
                new OfferInterest
                {
                    Id = 2,
                    OfferId = 1,
                    SeekerId = 2,
                    Date = DateTime.UtcNow.AddDays(-2)
                },
                new OfferInterest
                {
                    Id = 3,
                    OfferId = 2,
                    SeekerId = 3,
                    Date = DateTime.UtcNow.AddDays(-1)
                });
        }
    }
}
