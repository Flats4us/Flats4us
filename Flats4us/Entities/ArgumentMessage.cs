using Flats4us.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("ArgumentMessage")]
    public class ArgumentMessage  //not abstract
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string? Content { get; set; }

        public virtual OwnerStudent Sender { get; set; }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advertisement>().HasData(
            new ArgumentMessage
            {
                Id = 1,
                Date = DateTime.Now,
                Content = "First argument message",
            },
            new ArgumentMessage
            {
                Id = 2,
                Date = DateTime.Now.AddDays(-1),
                Content = "Second argument message"
            },
            new ArgumentMessage
            {
                Id = 3,
                Date = DateTime.Now.AddDays(-2),
                Content = "Third argument message"
            });
        }
    }
}
