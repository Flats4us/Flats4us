namespace Flats4us.Entities.Dto
{
    public class StudentForMatcherDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<InterestDto> Interests { get; set; }
        public string University { get; set; }
        public FileDto ProfilePicture { get; set; }
    }
}
