using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Grindr
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GrindrId { get; set; }


        public bool? isStudent1Interested { get; set; }


        public bool? isStudent2Interested { get; set; }

        [Required]
        public int Student1Id { get; set; }

        [Required]
        public int Student2Id { get; set; }


        public virtual Student Student1 { get; set; }
        public virtual Student Student2 { get; set; }
    }
}
