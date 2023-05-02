using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("Intervention")]
    public class Intervention  //not abstract
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public DateTime Date { get; set; }


        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<Intervention>().HasData(
            new Intervention
            {
                Id = 1,
                Description = "Intervention 1",
                Date = new DateTime(2023, 04, 12)
            },
            new Intervention
            {
                Id = 2,
                Description = "Intervention 2",
                Date = new DateTime(2023, 05, 01)
            },
            new Intervention
            {
                Id = 3,
                Description = "Intervention 3",
                Date = new DateTime(2023, 01, 20)
            }
            );
        }
    }
}
