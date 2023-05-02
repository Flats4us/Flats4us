using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("PropertyEquipment")]
    public class PropertyEquipment 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Required]
        public int EquipmentId { get; set; }

        public virtual Property Property { get; set; }
        public virtual Equipment Equipment { get; set; }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PropertyEquipment>().HasData(
            new PropertyEquipment
            {
                Id = 1,
                PropertyId = 1,
                EquipmentId = 1
            },
            new PropertyEquipment
            {
                Id = 2,
                PropertyId = 2,
                EquipmentId = 2
            },
            new PropertyEquipment
            {
                Id = 3,
                PropertyId = 3,
                EquipmentId = 3
            });
        }
    }
}
