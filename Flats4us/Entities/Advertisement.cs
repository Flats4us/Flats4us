using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("Advertisement")]
    public class Advertisement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public byte[] Image { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public virtual Moderator Moderator { get; set; }

    }
}
