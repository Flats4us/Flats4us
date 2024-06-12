using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{

        public class Alert
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int AlertId { get; set; }
            public string AlertName { get; set; }
            public string AlertBody { get; set; }
            public DateTime DateTime { get; set; }
            public bool Read { get; set; }
            public int UserId { get; set; }




    }
}
