using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class OfferDto
    {
        [Required]
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
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int NumberOfInterested { get; set; }

        [Required]
        public string Regulations { get; set; }

        [Required]
        public PropertyDto Property { get; set; }

        [Required]
        public OwnerStudentDto Owner { get; set; }

        [Required]
        public SurveyOwnerOfferDto SurveyOwnerOffer { get; set; }
    }
}
