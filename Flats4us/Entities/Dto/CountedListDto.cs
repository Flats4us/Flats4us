using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class CountedListDto<T>
    {
        [Required]
        public int TotalCount { get; set; }

        [Required]
        public List<T> Result { get; set; }

        public CountedListDto() { }

        public CountedListDto(List<T> items)
        {
            Result = items;
            TotalCount = items.Count;
        }

        public CountedListDto(List<T> items, int totalCount)
        {
            Result = items;
            TotalCount = totalCount;
        }
    }
}
