using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Flats4us.Helpers.Enums;

namespace Flats4us.Entities
{
    public class TechnicalProblem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TechnicalProblemId { get; set; }

        [Required]
        public TechnicalProblemEnum Kind { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool Solved { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
