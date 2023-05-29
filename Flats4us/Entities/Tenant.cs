using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Tenant : Student
    {
        [Required]
        public RoommatesStatus RoommatesStatus { get; set; }

        public virtual ICollection<Rent> Rents { get; set; }

        public Tenant()
        {
            this.Rents = new HashSet<Rent>();
        }
    }
}
