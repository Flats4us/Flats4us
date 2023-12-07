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
        public PaymentStatus PaymentStatus { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int RentId { get; set; }

        public virtual Student Student { get; set; }

        // TODO: Przypisanie powinno być chyba do wynajmu nie oferty

        public virtual Rent Rent { get; set; }
        //public virtual Offer Offer { get; set; }
    }
}
