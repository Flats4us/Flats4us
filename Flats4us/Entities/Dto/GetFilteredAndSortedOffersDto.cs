using Flats4us.Helpers.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class GetFilteredAndSortedOffersDto
    {
        [Required]
        public string Province { get; set; }

        [Required]
        public string City { get; set; }

        public int? Distance { get; set; }

        public List<PropertyType>? PropertyTypes { get; set; }

        public int? MinPrice { get; set; }

        public int? MaxPrice { get; set; }

        public string? District { get; set; }

        public int? MinArea { get; set; }

        public int? MaxArea { get; set; }

        public int? MinYear { get; set; }

        public int? MaxYear { get; set; }

        public int? MinNumberOfRooms { get; set; }

        public int? Floor { get; set; }

        public List<EquipmentDto>? Equipment { get; set; }

        [Required]
        public string Sorting { get; set; }

        [Required]
        public int PageNumber { get; set; }

        [Required]
        public int  PageSize { get; set; }
    }
}
