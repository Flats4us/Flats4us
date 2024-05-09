using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class OfferInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OfferInterestId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int OfferId { get; set; }

        public virtual Student Student { get; set; }

        public virtual Offer Offer { get; set; }
    }
}
