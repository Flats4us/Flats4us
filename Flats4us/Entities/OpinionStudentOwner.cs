using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class OpinionStudentOwner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OpinionOwnerStudentId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public int Check1 { get; set; }

        public int EvaluatedId { get; set; }

        public int EvaluatorId { get; set; }

        public Owner Evaluated { get; set; }

        public Student Evaluator { get; set; }
    }
}
