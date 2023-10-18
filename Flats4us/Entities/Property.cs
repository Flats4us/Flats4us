using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public abstract class Property
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PropertyId { get; set; }

        [Required]
        public string Province { get; set; }

        public string? District { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string Number { get; set; }

        public int? Flat { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public int Area { get; set; }

        [Required]
        public int MaxNumberOfInhabitants { get; set; }

        [Required]
        public int ConstructionYear { get; set; }

        [Required]
        public string ImagesPath { get; set; }

        [Required]
        public VerificationStatus VerificationStatus { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        public virtual ICollection<Equipment> Equipment { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }

        public Property()
        {
            this.Equipment = new HashSet<Equipment>();
            this.Offers = new HashSet<Offer>();
        }
    }
}
