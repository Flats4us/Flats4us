using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Flats4us.Entities
{
    [Table("PropertyImage")]
    public class PropertyImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Title { get; set; }

        // serwer plikow
        [Required]
        public byte[] ImageData { get; set; }

        public virtual Property Property { get; set; }
    }
}
