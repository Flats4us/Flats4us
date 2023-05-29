using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Advertisement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdvertisementId { get; set; }

        [Required]
        public string BannerPath { get; set; }

        [Required]
        public int Price { get; set; }

        // In Days
        [Required]
        public int Duration { get; set; }

        public Moderator Moderator { get; set; }
    }
}
