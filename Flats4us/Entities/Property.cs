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

        public string District { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public int Number { get; set; }

        public int Flat { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public int Area { get; set; }

        // TODO: Zdjęcia przchowywałbym w odpowiednich folderach po id nieruchomości

        [Required]
        public int MaxNumberOfInhabitants { get; set; }

        [Required]
        public int ConstructionYear { get; set; }

        [Required]
        public bool Elevator { get; set; }

        [Required]
        public string TitleDeedPath { get; set; }

        [Required]
        public VerificationStatus VerificationStatus { get; set; }

        public virtual ICollection<Equipment> Equipment { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }

        public Property()
        {
            this.Equipment = new HashSet<Equipment>();
            this.Offers = new HashSet<Offer>();
        }
    }
}
