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

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flat>().HasData(
            new Flat { 
                Id = 1, 
                Address = "ul. Wiejska 1, Warszawa", 
                Surface = 70, 
                MaxInhabitants = 2,
                NumberOfRooms = 1 
            },
            new Flat { 
                Id = 2, 
                Address = "ul. Wrocławska 12, Kraków", Surface = 120, MaxInhabitants = 4, NumberOfRooms = 1 },
            new Flat { Id = 3, Address = "ul. Kościuszki 50, Gdańsk", Surface = 90, MaxInhabitants = 3, NumberOfRooms = 1 });
        }
    }
}
