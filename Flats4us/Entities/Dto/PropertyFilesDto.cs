using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class PropertyFilesDto
    {
        [Required]
        public IFormFile? TitleDeed { get; set; }

        [Required]
        public List<IFormFile>? Images { get; set; }
    }
}
