using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class RentDto
    {
        [Required]
        public int RentId { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Required]
        public int OfferId { get; set; }

        [Required]
        public bool IsFinished { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string PropertyAddress { get; set; }

        [Required]
        public PropertyType PropertyType { get; set; }

        [Required]
        public List<FileDto> PropertyImages { get; set; }

        [Required]
        public ICollection<UserInfoDto> Tenants { get; set; }

        [Required]
        public ICollection<PaymentDto> Payments { get; set; }

        [Required]
        public virtual ICollection<ArgumentDto> Arguments { get; set; }
    }
}
