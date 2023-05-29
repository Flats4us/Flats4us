using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Equipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EquipmentId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Property> Properties { get; set; }

        public Equipment()
        {
            this.Properties = new HashSet<Property>();
        }
    }
}
