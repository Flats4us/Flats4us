using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Matcher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MatcherId { get; set; }

        public bool? IsStudent1Interested { get; set; }

        public bool? IsStudent2Interested { get; set; }

        public static double AgreementPercentage { get; set; } = 0.8;

        [Required]
        public int Student1Id { get; set; }

        [Required]
        public int Student2Id { get; set; }

        public virtual Student Student1 { get; set; }
        public virtual Student Student2 { get; set; }
    }
}
