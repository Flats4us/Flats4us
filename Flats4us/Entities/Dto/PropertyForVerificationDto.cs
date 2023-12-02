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
        public List<string> ImagesURLs { get; set; }

        [Required]
        public string DocumentURL { get; set; }

        [Required]
        public VerificationStatus VerificationStatus { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}
