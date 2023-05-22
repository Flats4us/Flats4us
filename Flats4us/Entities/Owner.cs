using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Owner : OwnerStudent
    {
        [Required]
        public string BankAccount { get; set; }

        [Required]
        public string TitleDeedPath { get; set; }

        public virtual ICollection<OpinionOwnerStudent> IssuedOwnerStudentOpinions { get; set; }
        public virtual ICollection<OpinionStudentOwner> ReceivedStudentOwnerOpinions { get; set; }

        public Owner()
        {
            this.IssuedOwnerStudentOpinions = new HashSet<OpinionOwnerStudent>();
            this.ReceivedStudentOwnerOpinions= new HashSet<OpinionStudentOwner>();
        }
    }
}
