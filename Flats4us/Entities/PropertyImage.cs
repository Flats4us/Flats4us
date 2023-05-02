using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Flats4us.Entities
{
    [Table("PropertyImage")]
    public class PropertyImage  //to jest do property
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Title { get; set; }

        [Required]
        public string? ImagePath { get; set; }

        public virtual Property Property { get; set; }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PropertyImage>().HasData(
            new PropertyImage { Id = 1, Title = "Living room", ImagePath = "images/livingroom.jpg" },
            new PropertyImage { Id = 2, Title = "Kitchen", ImagePath = "images/kitchen.jpg" },
            new PropertyImage { Id = 3, Title = "Bedroom", ImagePath = "images/bedroom.jpg" });
        }
    }
}
