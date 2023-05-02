using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class House : Property //not abstract
    {
        [Required]
        [Column("NumberOfRooms")]
        public int NumberOfRooms { get; set; }

        [Required]
        public int NumberOfFloors { get; set; }

        [Required]
        public int ParcelSurface { get; set; }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<House>().HasData(
            new House
            {
                Id = 4,
                Address = "ul. Mickiewicza 20, Kraków",
                Surface = 150,
                MaxInhabitants = 6,
                NumberOfRooms = 5,
                NumberOfFloors = 2,
                ParcelSurface = 500
            },
            new House
            {
                Id = 5,
                Address = "ul. Długa 15, Gdańsk",
                Surface = 100,
                MaxInhabitants = 4,
                NumberOfRooms = 4,
                NumberOfFloors = 1,
                ParcelSurface = 300
            },
            new House
            {
                Id = 6,
                Address = "ul. Jagiellońska 10, Warszawa",
                Surface = 200,
                MaxInhabitants = 8,
                NumberOfRooms = 6,
                NumberOfFloors = 3,
                ParcelSurface = 700
            });
        }
    }
}
