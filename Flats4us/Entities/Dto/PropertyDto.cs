using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities.Dto
{
    public class PropertyDto
    {
        [Required]
        public int PropertyId { get; set; }

        [Required]
        public PropertyType PropertyType { get; set; }

        [Required]
        public string Province { get; set; }

        public string? District { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public int Number { get; set; }

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

        [Required]
        public bool Elevator { get; set; }

        [Required]
        public string ImagesPath { get; set; }

        [Required]
        public VerificationStatus VerificationStatus { get; set; }

        public int? NumberOfRooms { get; set; }

        public int? NumberOfFloors { get; set; }

        public int? PlotArea { get; set; }

        public int? Floor { get; set; }
    }
}
