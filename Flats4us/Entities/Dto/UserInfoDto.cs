namespace Flats4us.Entities.Dto
{
    public class UserInfoDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public FileDto ProfilePicture { get; set; }
    }
}
