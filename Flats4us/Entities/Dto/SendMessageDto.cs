using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class SendMessageDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
