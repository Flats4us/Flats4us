using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class FileDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Path { get; set; }
    }
}
