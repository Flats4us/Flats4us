using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class AddTechnicalProblemDto
    {
        [Required]
        public TechnicalProblemType Kind { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
