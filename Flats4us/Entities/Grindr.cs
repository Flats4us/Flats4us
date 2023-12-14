using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Grindr
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GrindrId { get; set; }

        [Required]
        public bool isStudent1Interested { get; set; }

        [Required]
        public bool isStudent2Interested { get; set; }

        [Required]
        public int Student1Id { get; set; }

        [Required]
        public int Student2Id { get; set; }


        public Student Student1 { get; set; }
        public Student Stundet2 { get; set; }
    }
}
