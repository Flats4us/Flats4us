using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class SurveyOwnerOfferDto
    {
        [Required]
        public bool Smoking { get; set; }

        [Required]
        public bool Parties { get; set; }

        [Required]
        public bool Animals { get; set; }

        [Required]
        public Gender Gender { get; set; }
    }
}
