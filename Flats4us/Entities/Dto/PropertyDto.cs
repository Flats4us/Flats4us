﻿using Flats4us.Helpers.Enums;
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
        public string Number { get; set; }

        public int? Flat { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public double GeoLat { get; set; }

        [Required]
        public double GeoLon { get; set; }

        [Required]
        public int Area { get; set; }

        [Required]
        public int MaxNumberOfInhabitants { get; set; }

        [Required]
        public int ConstructionYear { get; set; }

        [Required]
        public List<FileDto> Images { get; set; }

        [Required]
        public decimal AvgRating { get; set; }

        [Required]
        public decimal AvgServiceRating { get; set; }

        [Required]
        public decimal AvgLocationRating { get; set; }

        [Required]
        public decimal AvgEquipmentRating { get; set; }

        [Required]
        public decimal AvgQualityForMoneyRating { get; set; }

        [Required]
        public VerificationStatus VerificationStatus { get; set; }

        public int? NumberOfRooms { get; set; }

        public int? NumberOfFloors { get; set; }

        public int? PlotArea { get; set; }

        public int? Floor { get; set; }

        public List<SimpleOfferForPropertyDetailsDto> Offers { get; set; }

        public ICollection<EquipmentDto> Equipment { get; set; }
        public ICollection<RentOpinionDto> RentOpinions { get; set; }
    }
}
