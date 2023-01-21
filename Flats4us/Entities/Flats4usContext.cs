using Microsoft.EntityFrameworkCore;

namespace Flats4us.Entities
{
    public class Flats4usContext : DbContext
    {
        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<Flat> Flats { get; set; } 
        public virtual DbSet<Rent> Rents { get; set; }

        public Flats4usContext() { }

        public Flats4usContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>().HasData( 
                new 
                {   TenantId = 1,
                    Name = "Jan",
                    Surname = "Kowalski",
                    AddressLine1 = "ul. Dluga 1",
                    AddressLine2 = "",
                    AddressLine3 = "",
                    Email = "jan.kowalski@gmail.com",
                    PhoneNumber = "123456789"
                },
                new
                {
                    TenantId = 2,
                    Name = "Maciej",
                    Surname = "Nowak",
                    AddressLine1 = "ul. Dluga 45",
                    AddressLine2 = "",
                    AddressLine3 = "",
                    Email = "maciej.nowak@gmail.com",
                    PhoneNumber = "987654321"
                }
            );

            modelBuilder.Entity<Flat>().HasData(
                new
                {
                    FlatId = 1,
                    Name = "Mieszkanie 1",
                    AddressLine1 = "ul. Dluga 1",
                    AddressLine2 = "",
                    AddressLine3 = "",
                    MetricArea = 40.0F,
                    MaxNumberOfInhabitants = 5
                },
                new
                {
                    FlatId = 2,
                    Name = "Mieszkanie 2",
                    AddressLine1 = "ul. Dluga 45",
                    AddressLine2 = "",
                    AddressLine3 = "",
                    MetricArea = 50.0F,
                    MaxNumberOfInhabitants = 4
                }
            );

            modelBuilder.Entity<Rent>().HasData(
                new
                {
                    RentId = 1,
                    TenantId = 1,
                    FlatId = 2,
                    StartDate = new DateTime(2022, 10, 25),
                    DurationInMonths = 10,
                    PricePerMonth = 2000.0F,
                },
                new
                {
                    RentId = 2,
                    TenantId = 2,
                    FlatId = 1,
                    StartDate = new DateTime(2022, 11, 5),
                    DurationInMonths = 6,
                    PricePerMonth = 2000.0F,
                }
            );

        }
    }
}
