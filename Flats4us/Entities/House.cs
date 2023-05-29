using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class House : Property
    {
        [Required]
        public int NumberOfRooms { get; set; }

        [Required]
        public int NumberOfFloors { get; set; }

        [Required]
        public int PlotArea { get; set; }
    }
}
