namespace Flats4us.Entities.Dto
{
    public class ConsentDto
    {
        public bool PushChatConsent { get; set; }
        public bool EmailChatConsent { get; set; }

        public bool PushPropertyConsent { get; set; }
        public bool EmailPropertyConsent { get; set; }
    }
}
