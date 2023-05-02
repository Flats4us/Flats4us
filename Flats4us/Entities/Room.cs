using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class Room : Property //not abstract
    {
        [Required]
        public string? Name { get; set; }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1,
                Address = "ul. Wiejska 1, Warszawa", 
                Surface = 70, 
                MaxInhabitants = 2,
                Name = "Największy" 
            },
            new Room { Id = 2,
                Address = "ul. Wrocławska 12, Kraków",
                Surface = 120,
                MaxInhabitants = 4, 
                Name = "Najmniejszy"
            },
            new Room { Id = 3, 
                Address = "ul. Kościuszki 50, Gdańsk", 
                Surface = 90, 
                MaxInhabitants = 3,
                Name = "Najśredniejszy"
            });
        }
    }
}
