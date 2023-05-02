using Flats4us.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("Equipment")]
    public class Equipment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string? Name { get; set; }
        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipment>().HasData(
            new Equipment { Id = 1, Name = "Laptop" },
            new Equipment { Id = 2, Name = "Projector" },
            new Equipment { Id = 3, Name = "Microphone" });
        }
    }
}
