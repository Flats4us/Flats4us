using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class UserOpinion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserOpinionId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(1, 10)]
        public int Rating { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int SourceUserId { get; set; }

        [Required]
        public int TargetUserId { get; set; }

        public virtual OwnerStudent SourceUser { get; set; }

        public virtual OwnerStudent TargetUser { get; set; }
    }
}
