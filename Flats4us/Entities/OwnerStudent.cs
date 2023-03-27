using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public enum DocumentType
    {
        ID,
        StudentCard,
        Passport
    }

    public enum VerificationStatus
    {
        Verified,
        NotVerified
    }

    public class OwnerStudent : User
    {
        [Required]
        public byte[] Photo { get; set; }

        [Required]
        public bool ActivityStatus { get; set; }

        // ???
        [Required]
        public string? DocumentPath { get; set; }

        [Required]
        public DocumentType DocumentType { get; set; }

        [Required]
        public VerificationStatus VerificationStatus { get; set; }

        [Required]
        public DateTime DocumentExpireDate { get; set; }

        //public virtual ICollection<PersonOpinion> ReceivedRatings { get; set; }
        //public virtual ICollection<PersonOpinion> IssuedRatings { get; set; }

        public OwnerStudent()
        {
            //ReceivedRatings = new HashSet<PersonOpinion>();
           // IssuedRatings = new HashSet<PersonOpinion>();
        }
    }
}
