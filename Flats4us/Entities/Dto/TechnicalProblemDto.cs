using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class TechnicalProblemDto
    {
        [Required]
        public int TechnicalProblemId { get; set; }

        [Required]
        public TechnicalProblemType Kind { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool Solved { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
