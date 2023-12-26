using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class PropertyForVerificationDto
    {
        [Required]
        public int PropertyId { get; set; }

        [Required]
        public PropertyType PropertyType { get; set; }

        [Required]
        public string OwnerName { get; set; }

        [Required]
        public string OwnerEmail { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public List<FileDto> Images { get; set; }

        [Required]
        public FileDto Document { get; set; }

        [Required]
        public VerificationStatus VerificationStatus { get; set; }

        [Required]
        public DateTime DateForVerificationSorting { get; set; }
    }
}
