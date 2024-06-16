using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class ArgumentDto
    {
        public int ArgumentId { get; set; }

        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? OwnerAcceptanceDate { get; set; }

        public DateTime? StudentAccceptanceDate { get; set; }


        [Required]
        public ArgumentStatus ArgumentStatus { get; set; }

        public bool InterventionNeed { get; set; }

        public DateTime? InterventionNeedDate { get; set; }

        public int ArgumentCreatedByUserId { get; set; }

        [Required]
        public int GroupChatId { get; set; }

        [Required]
        public UserInfoDto Student { get; set; }
        public UserInfoDto Owner { get; set; }

        public virtual ICollection<ArgumentInterventionDto> ArgumentInterventions { get; set; }

        public RentForArgumentDto Rent { get; set; }

    }
}
