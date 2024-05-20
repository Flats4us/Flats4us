using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public abstract class OwnerStudent : User
    {
        public File? ProfilePicture { get; set; }

        public File? Document { get; set; }

        [Required]
        public DocumentType DocumentType { get; set; }

        [Required]
        public DateTime DocumentExpireDate { get; set; }

        [Required]
        public DateTime DateForVerificationSorting { get; set; }

        public virtual ICollection<UserOpinion> IssuedUserOpinions { get; set; }
        public virtual ICollection<UserOpinion> ReceivedUserOpinions { get; set; }


    }
}
