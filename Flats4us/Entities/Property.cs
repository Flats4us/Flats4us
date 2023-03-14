using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("Property")]
    public class Property
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Address { get; set; }

        public int Surface { get; set; }

        public int MaxInhabitants { get; set; }


    }
}
