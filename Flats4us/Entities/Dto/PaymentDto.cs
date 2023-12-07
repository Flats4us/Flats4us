using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class PaymentDto
    {
        [Required]
        public PaymentPurpose PaymentPurpose { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int RentId { get; set; }
    }
}
