using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class Connection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ContextConnectionId { get; set; }
        public string HubName { get; set; }
    }
}
