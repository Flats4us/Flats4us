using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class Room : Property
    {
        [Required]
        public string? Name { get; set; }
    }
}
