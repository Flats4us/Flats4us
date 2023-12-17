using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class CountedListDto<T>
    {
        [Required]
        public int TotalCount { get; set; }

        [Required]
        public List<T> Result { get; set; }
    }
}
