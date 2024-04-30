using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class PaymentDto
    {
        [Required]
        public int PaymentId { get; set; }

        [Required]
        public PaymentPurpose PaymentPurpose { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public bool IsPaid { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }
    }
}
