namespace Flats4us.Entities.Dto
{
    public class ChatMessageDto
    {
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; } // Optional: Include if you want to show who sent the message

    }

}
