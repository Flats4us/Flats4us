using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Flats4us.Helpers.Enums;

namespace Flats4us.Entities
{
    public class ChatMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChatMessageId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        //public int SenderUserId { get; set; }
        public virtual User Sender { get; set; }
        //public int? GroupChatId { get; set; }
        //public int? ChatId { get; set; }
        public virtual GroupChat? GroupChat { get; set; }

        public virtual Chat? Chat { get; set; }
    }
}
