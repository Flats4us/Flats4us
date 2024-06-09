using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class RentForArgumentDto
    {
        [Required]
        public int RentId { get; set; }

        [Required]
        public string PropertyAddress { get; set; }
    }
}
