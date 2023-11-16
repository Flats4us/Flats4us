using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class ArgumentDto
    {
        [Required]
        public int ArgumentId { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? StudentAcceptanceDate { get; set; }

        public DateTime? OwnerAcceptanceDate { get; set; }

        [Required]
        public ArgumentStatus ArgumentStatus { get; set; }

        public DateTime? MederatorDecisionDate { get; set; }
    }
}
