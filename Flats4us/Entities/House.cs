using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class House : Property
    {
        [Required]
        [Column("NumberOfRooms")]
        public int NumberOfRooms { get; set; }

        [Required]
        public int NumberOfFloors { get; set; }

        [Required]
        public int PlotArea { get; set; }
    }
}
