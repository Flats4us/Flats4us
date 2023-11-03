using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class EquipmentDto
    {
        [Required]
        public int EquipmentId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
