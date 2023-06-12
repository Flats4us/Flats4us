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

        public Student Student { get; set; }

        public Offer Offer { get; set; }
    }
}
