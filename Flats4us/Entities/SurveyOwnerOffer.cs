using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Flats4us.Helpers.Enums;
using Helpers;

namespace Flats4us.Entities
{
    public class SurveyOwnerOffer
    {
        [ForeignKey("Offer")]
        [SurveyIgnore]
        public int SurveyOwnerOfferId { get; set; }

        [Required]
        public bool SmokingAllowed { get; set; }

        [Required]
        public bool PartiesAllowed { get; set; }

        [Required]
        public bool AnimalsAllowed { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [SurveyIgnore]
        public int OfferId { get; set; }

        public virtual Offer Offer { get; set; }
    }
}
