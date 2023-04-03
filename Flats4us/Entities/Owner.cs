using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Owner : OwnerStudent
    {
        [Required]
        public string? BankAccount { get; set; }

        [Required]
        public string? TitleDeedPath { get; set; }

        public virtual ICollection<OwnerOpinion> IssuedRatings { get; set; }

        public virtual ICollection<StudentOpinion> ReceivedRatings { get; set; }

        public Owner()
        {
            IssuedRatings = new HashSet<OwnerOpinion>();
            ReceivedRatings = new HashSet<StudentOpinion>();
        }
    }
}
