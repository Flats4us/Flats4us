using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public enum Department
    {
        dept1,
        dept2
    }

    public class Moderator : User
    {
        [Required]
        public DateTime HireDate { get; set; }

        [Required]
        public Department Department { get; set; }

        public virtual ICollection<Intervention> Interventions { get; set; }

        public Moderator()
        {
            Interventions = new HashSet<Intervention>();
        }
    }
}