using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public enum PromotionType
    {
        type1,
        type2
    }

    [Table("Promotion")]
    public class Promotion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public PromotionType PromotionType { get; set; }

        public virtual Offer Offer { get; set; }
    }
}
