using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("Property")]
    public class Property //abstract 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public int Surface { get; set; }

        [Required]
        public int MaxInhabitants { get; set; }


        public virtual ICollection<PropertyEquipment> PropertyEquipments { get; set; }
        public virtual ICollection<PropertyImage> PropertyImages { get; set; }

        public Property()
        {
            PropertyEquipments = new HashSet<PropertyEquipment>();
            PropertyImages = new HashSet<PropertyImage>();
        }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>().HasData(
            new Property { Id = 1, Address = "ul. Wiejska 1, Warszawa", Surface = 70, MaxInhabitants = 2 },
            new Property { Id = 2, Address = "ul. Wrocławska 12, Kraków", Surface = 120, MaxInhabitants = 4 },
            new Property { Id = 3, Address = "ul. Kościuszki 50, Gdańsk", Surface = 90, MaxInhabitants = 3 });
        }

    }
}
