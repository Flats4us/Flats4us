using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class Flat : Property
    {
        [Required]
        [Column("NumberOfRooms")]
        public int NumberOfRooms { get; set; }

        [Required]
        [Column("Floor")]
        public int Floor { get; set; }

        [Required]
        [Column("Elevator")]
        public bool Elevator { get; set; }
    }
}
