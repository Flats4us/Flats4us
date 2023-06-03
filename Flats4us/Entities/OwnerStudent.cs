using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public abstract class OwnerStudent : User
    {
        [Required]
        public string PhotoPath { get; set; }

        [Required]
        public bool ActivityStatus { get; set; }

        [Required]
        public string DocumentPath { get; set; }

        [Required]
        public DocumentType DocumentType { get; set; }

        [Required]
        public VerificationStatus VerificationStatus { get; set; }

        [Required]
        public DateTime DocumentExpireDate { get; set; }
    }
}
