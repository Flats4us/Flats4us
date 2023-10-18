using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class Room : Property
    {
        [Required]
        [Column("Floor")]
        public int Floor { get; set; }

        [Required]
        [Column("Elevator")]
        public bool Elevator { get; set; }
    }
}
