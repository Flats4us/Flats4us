using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("Property")]
    public class Property
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

    }
}
