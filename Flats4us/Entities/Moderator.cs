using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public enum Department
    {
        dept1,
        dept2
    }

    public class Moderator : User //not abstract
    {
        [Required]
        public DateTime HireDate { get; set; }

        [Required]
        public Department Department { get; set; }

        public virtual ICollection<Intervention> Interventions { get; set; }

        public Moderator()
        {
            Interventions = new HashSet<Intervention>();
        }


        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Moderator>().HasData(
            new Moderator
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Address = "123 Main St",
                City = "New York",
                PhoneNumber = "555-1234",
                Email = "john.doe@example.com",
                AccountCreationDate = new DateTime(2023, 4, 10),
                LastLoginDate = new DateTime(2023, 4, 29),
                Password = "password123",
                HireDate = DateTime.Parse("2023-04-10"),
                Department = Department.dept1
            },
            new Moderator
            {
                Id = 2,
                Name = "Jane",
                Surname = "Smith",
                Address = "456 Park Ave",
                City = "Los Angeles",
                PhoneNumber = "555-5678",
                Email = "jane.smith@example.com",
                AccountCreationDate = new DateTime(2023, 3, 15),
                LastLoginDate = new DateTime(2023, 4, 24),
                Password = "password456",
                HireDate = new DateTime(2023, 2, 25),
                Department = Department.dept2
            },
            new Moderator
            {
                Id = 3,
                Name = "Bob",
                Surname = "Johnson",
                Address = "789 5th Ave",
                City = "Chicago",
                PhoneNumber = "555-9012",
                Email = "bob.johnson@example.com",
                AccountCreationDate = DateTime.Parse("2023-01-15"),
                LastLoginDate = DateTime.Parse("2023-04-22"),
                Password = "password789",
                HireDate = DateTime.Parse("2023-01-14"),
                Department = Department.dept1
            });
        }
    }
}