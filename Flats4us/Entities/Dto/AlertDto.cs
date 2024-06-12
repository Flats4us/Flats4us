namespace Flats4us.Entities.Dto
{
    public class AlertDto
    {
        
        public int AlertId { get; set; }
        public string AlertName { get; set; }
        public string AlertBody { get; set; }
        public DateTime DateTime { get; set; }
        public bool Read { get; set; }
        public int UserId { get; set; }
    
    }
}
