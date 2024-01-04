using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class TechnicalProblemDto
    {
        [Required]
        public TechnicalProblem Problem { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int UserId { get; set; }
    }
}
