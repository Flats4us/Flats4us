namespace Flats4us.Entities.Dto
{
    public class CreateGroupChatDto
    {
        public string GroupName { get; set; }
        public List<int> UserIds { get; set; }
    }
}
