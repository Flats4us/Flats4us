using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class PropertyFilesDto
    {
        public IFormFile? TitleDeed { get; set; }

        public List<IFormFile>? Images { get; set; }
    }
}
