using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Room : Property
    {
        [Required]
        public string Name { get; set; }
    }
}
