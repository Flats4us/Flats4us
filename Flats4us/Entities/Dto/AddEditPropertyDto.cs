using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class AddEditPropertyDto
    {
        [Required]
        public PropertyType PropertyType { get; set; }

        [Required]
        public string Province { get; set; }

        public string? District { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string Number { get; set; }

        public int? Flat { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public int Area { get; set; }

        [Required]
        public int MaxNumberOfInhabitants { get; set; }

        [Required]
        public int ConstructionYear { get; set; }

        public int? NumberOfRooms { get; set; }

        public int? Floor { get; set; }

        public int? NumberOfFloors { get; set; }

        public int? PlotArea { get; set; }

        [Required]
        public List<int> EquipmentIds { get; set; }
    }
}
