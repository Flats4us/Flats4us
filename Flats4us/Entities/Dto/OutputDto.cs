namespace Flats4us.Entities.Dto
{
    public class OutputDto<T>
    {
        public T Result { get; set; }

        public OutputDto(T result)
        {
            Result = result;
        }
    }
}
