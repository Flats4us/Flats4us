using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("Advertisement")]
    public class Advertisement //not abstract
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



        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advertisement>().HasData(
            new Advertisement
            {
                Id = 1,
                Image = new byte[] { 0x12, 0x34, 0x56, 0x78 },
                Price = 100,
                EndDate = DateTime.Parse("2023-05-15")
            },
            new Advertisement
            {
                Id = 2,
                Image = new byte[] { 0x12, 0x34, 0x56, 0x78 },
                Price = 250,
                EndDate = DateTime.Parse("2023-05-20")
            },
            new Advertisement
            {
                Id = 3,
                Image = new byte[] { 0x12, 0x34, 0x56, 0x78 },
                Price = 470,
                EndDate = DateTime.Parse("2023-06-29")
            }
            );
        }
    }
}
