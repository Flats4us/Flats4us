namespace Flats4us.Entities.Dto
{
    public class ChatMessageDto
    {
        public int ChatMessageId { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public int SenderId { get; set; }
    }
}
