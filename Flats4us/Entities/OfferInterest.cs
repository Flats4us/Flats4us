using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("OfferInterest")]
    public class OfferInterest  //not abstract
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int OfferId { get; set; }

        [Required]
        public int SeekerId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual Offer Offer { get; set; }
        public virtual Seeker Seeker { get; set; }

    }
}
