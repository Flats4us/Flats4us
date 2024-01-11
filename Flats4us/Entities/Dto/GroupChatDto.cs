namespace Flats4us.Entities.Dto
{
    public class GroupChatDto
    {
        public int GroupChatId { get; set; }
        public string Name { get; set; }
        public List<UserInfoDto> Users { get; set; }
    }
}