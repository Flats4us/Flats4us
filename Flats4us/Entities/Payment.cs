using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class Payment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }

        [Required]
        public PaymentPurpose PaymentPurpose { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public bool IsPaid {  get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        public virtual Rent Rent { get; set; }
    }
}
