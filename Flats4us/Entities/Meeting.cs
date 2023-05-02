using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("Meeting")]
    public class Meeting //not abstract
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? Reason { get; set; }

        public virtual Offer Offer { get; set; }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meeting>().HasData(
            new Meeting
            {
                Id = 1,
                Date = DateTime.Now,
                Address = "First meeting address",
                Reason = "First meeting reason"
            },
            new Meeting
            {
                Id = 2,
                Date = DateTime.Now.AddDays(1),
                Address = "Second meeting address",
                Reason = "Second meeting reason"
            },
            new Meeting
            {
                Id = 3,
                Date = DateTime.Now.AddDays(2),
                Address = "Third meeting address",
                Reason = "Third meeting reason"
            });

        }
    }
}
