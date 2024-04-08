using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class RentOpinion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentOpinionId { get; set; }

        [Required]
        [Range(1, 10)]
        public int Rating { get; set; }

        [Required]
        [Range(1, 10)]
        public int Service { get; set; }

        [Required]
        [Range(1, 10)]
        public int Location { get; set; }

        [Required]
        [Range(1, 10)]
        public int Equipment { get; set; }

        [Required]
        [Range(1, 10)]
        public int QualityForMoney { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int RentId { get; set; }

        public virtual Student Student { get; set; }

        public virtual Rent Rent { get; set; }
    }
}
