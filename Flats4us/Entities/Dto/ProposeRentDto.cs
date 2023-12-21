using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class ProposeRentDto
    {
        [Required]
        public List<string> RoommatesEmails { get; set; }
    }
}
