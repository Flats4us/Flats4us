using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Flat : Property
    {
        [Required]
        public int NumberOfRooms { get; set; }

        [Required]
        public int Floor { get; set; }
    }
}
