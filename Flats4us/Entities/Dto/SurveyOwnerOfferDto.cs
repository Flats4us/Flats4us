using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class SurveyOwnerOfferDto
    {
        [Required]
        public bool SmokingAllowed { get; set; }

        [Required]
        public bool PartiesAllowed { get; set; }

        [Required]
        public bool AnimalsAllowed { get; set; }

        [Required]
        public Gender Gender { get; set; }
    }
}
