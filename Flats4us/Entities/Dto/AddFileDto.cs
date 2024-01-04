using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class AddFileDto
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
