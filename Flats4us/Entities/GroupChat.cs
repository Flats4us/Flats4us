using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class GroupChat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GroupChatId { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        public virtual ICollection<ChatMessage> GroupChatMessages { get; set; }
        public virtual ICollection<UserGroupChat> UserGroupChats { get; set; }

        public GroupChat()
        {
            this.GroupChatMessages = new HashSet<ChatMessage>();    
            this.UserGroupChats = new HashSet<UserGroupChat>();
        }
    }
}
