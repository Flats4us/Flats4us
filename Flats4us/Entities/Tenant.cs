using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Tenant : Student
    {
        [Required]
        public RoommatesStatus RoommatesStatus { get; set; }
    }
}
