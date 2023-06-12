using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class OpinionStudentStudent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OpinionStudentStudentId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public int Check1 { get; set; }

        [Required]
        public int EvaluatedId { get; set; }

        [Required]
        public int EvaluatorId { get; set; }

        public virtual Student Evaluated { get; set; }

        public virtual Student Evaluator { get; set; }
    }
}
