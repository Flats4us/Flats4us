using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class SimplePropertyDto
    {
        [Required]
        public double GeoLat { get; set; }

        [Required]
        public double GeoLon { get; set; }

        [Required]
        public PropertyType PropertyType { get; set; }

        public ICollection<EquipmentDto> Equipment { get; set; }
    }
}
