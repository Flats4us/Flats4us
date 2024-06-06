using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Chat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChatId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public virtual Student Student { get; set; }

        public virtual Owner Owner { get; set; }

        public virtual ICollection<ChatMessage> ChatMessages { get; set; }

        public Chat()
        {
            this.ChatMessages = new HashSet<ChatMessage>();
        }
    }
}
