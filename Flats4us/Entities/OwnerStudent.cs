using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public abstract class OwnerStudent : User
    {
        public FileUpload? ProfilePicture { get; set; }

        public FileUpload? Document { get; set; }

        [Required]
        public DocumentType DocumentType { get; set; }

        [Required]
        public DateTime DocumentExpireDate { get; set; }

        public DateTime? DateForVerificationSorting { get; set; }

        public DateTime? VerificationOrRejectionDate { get; set; }

        public virtual ICollection<UserOpinion> IssuedUserOpinions { get; set; }
        public virtual ICollection<UserOpinion> ReceivedUserOpinions { get; set; }


    }
}
