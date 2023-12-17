using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class PaginatorDto
    {
        [Required]
        public int PageNumber { get; set; }

        [Required]
        public int PageSize { get; set; }
    }
}
