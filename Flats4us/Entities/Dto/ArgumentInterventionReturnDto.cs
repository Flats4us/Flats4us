using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class ArgumentInterventionReturnDto
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Justification { get; set; }

        [Required]
        public int ArgumentId { get; set; }

        [Required]
        public int ModeratorId { get; set; }
    }
}
