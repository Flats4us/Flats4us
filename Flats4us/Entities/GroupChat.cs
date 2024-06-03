using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class GroupChat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GroupChatId { get; set; }

        [MaxLength(100)] // Optional: Max length for group chat name
        public string Name { get; set; } // Optional: Name of the group chat

        // Navigation property for messages in the group chat
        //public virtual ICollection<ChatMessage> GroupChatMessages { get; set; }

        // Navigation property for users in the group chat

        public virtual ICollection<UserGroupChat> UserGroupChats { get; set; }

        public virtual Argument Argument { get; set; }

        public GroupChat()
        {
            //this.GroupChatMessages = new HashSet<ChatMessage>();    
            this.UserGroupChats = new HashSet<UserGroupChat>();
        }
    }
}
