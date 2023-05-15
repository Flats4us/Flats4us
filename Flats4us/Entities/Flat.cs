using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class Flat : Property //not  abstract
    {
        [Required]
        [Column("NumberOfRooms")]
        public int NumberOfRooms { get; set; }


    }
}
