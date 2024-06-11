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
        public int User1Id { get; set; }

        [Required]
        public int User2Id { get; set; }

        public virtual User User1 { get; set; }

        public virtual User User2 { get; set; }

        public virtual ICollection<ChatMessage> ChatMessages { get; set; }

        public Chat()
        {
            this.ChatMessages = new HashSet<ChatMessage>();
        }
    }
}
