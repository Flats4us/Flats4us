using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class AddArgumentDto
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public int RentId { get; set; }
    }
}
