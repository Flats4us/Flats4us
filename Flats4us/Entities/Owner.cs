using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Owner : OwnerStudent
    {
        [Required]
        public string BankAccount { get; set; }

        [Required]
        public string DocumentNumber { get; set; }

        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<Property> Properties { get; set; }

        public Owner()
        {
            this.Chats = new HashSet<Chat>();
            this.Properties = new HashSet<Property>();
        }
    }
}
