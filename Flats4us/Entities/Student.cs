using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Student : OwnerStudent
    {
        [Required]
        public int YearOfBirth { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string? StudentNumber { get; set;}

        [Required]
        public string? University { get;}

        public virtual Survey Survey { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<OwnerOpinion> IssuedRatings { get; set; }
        public virtual ICollection<StudentOpinion> ReceivedRatings { get; set; }

        public Student()
        {
            Payments = new HashSet<Payment>();
            IssuedRatings = new HashSet<OwnerOpinion>();
            ReceivedRatings = new HashSet<StudentOpinion>();
        }

    }
}
