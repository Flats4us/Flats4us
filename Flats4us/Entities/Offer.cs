using Flats4us.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public enum OfferStatus
    {
        Current,
        Outdated,
        Suspended,
        Rented
    }

    [Table("Offer")]
    public class Offer //abstract
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public OfferStatus OfferStatus { get; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public int RentalPeriod { get; set; }

        [Required]
        public int NumberOfIntrested { get; set; }

        [Required]
        public string? Regulations { get; set; }

        public virtual Property Property { get; set; }

        public virtual ICollection<Rent> Rentals { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Promotion> Promotions { get; set; }

        public Offer()
        {
            Rentals = new HashSet<Rent>();
            Meetings = new HashSet<Meeting>();
            Payments = new HashSet<Payment>();
            Promotions = new HashSet<Promotion>();
        }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Offer>().HasData(
        new Offer
        {
            Id = 1,
            Date = DateTime.Now,
            Price = 1000,
            Description = "First offer",
            RentalPeriod = 12,
            NumberOfIntrested = 5,
            Regulations = "First offer regulations"
        },
        new Offer
        {
            Id = 2,
            Date = DateTime.Now.AddDays(-1),
            Price = 2000,
            Description = "Second offer",
            RentalPeriod = 6,
            NumberOfIntrested = 10,
            Regulations = "Second offer regulations"
        },
        new Offer
        {
            Id = 3,
            Date = DateTime.Now.AddDays(-2),
            Price = 1500,
            Description = "Third offer",
            RentalPeriod = 9,
            NumberOfIntrested = 3,
            Regulations = "Third offer regulations"
        });
        }
    }
}