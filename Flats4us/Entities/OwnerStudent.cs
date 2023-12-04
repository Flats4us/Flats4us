using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public abstract class OwnerStudent : User
    {
        [Required]
        public string ImagesPath { get; set; }

        [Required]
        public DocumentType DocumentType { get; set; }

        [Required]
        public DateTime DocumentExpireDate { get; set; }

        [Required]
        public DateTime DateForVerificationSorting { get; set; }
    }
}
