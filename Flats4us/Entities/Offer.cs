using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Flats4us.Helpers.Enums;

namespace Flats4us.Entities
{
    public class Offer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OfferId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public OfferStatus Status { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Deposit { get; set; }

        [Required]
        public string Description { get; set;}

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int NumberOfInterested { get; set; }

        [Required]
        public string Regulations { get; set; }

        [Required]
        public int PropertyId { get; set; }

        public virtual Property Property { get; set; }
        public virtual SurveyOwnerOffer SurveyOwnerOffer { get; set; }
        public virtual Rent Rent { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }
        public virtual ICollection<OfferInterest> OfferInterests { get; set; }
        public virtual ICollection<OfferPromotion> OfferPromotions { get; set; }
        public virtual ICollection<Argument> Arguments { get; set; }

        public Offer()
        {
            this.Payments = new HashSet<Payment>();
            this.Meetings = new HashSet<Meeting>();
            this.OfferInterests = new HashSet<OfferInterest>();
            this.OfferPromotions = new HashSet<OfferPromotion>();
            this.Arguments = new HashSet<Argument>();
        }
    }
}
