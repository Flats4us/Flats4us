using Microsoft.EntityFrameworkCore;

namespace Flats4us.Entities
{
    public class Flats4usContext : DbContext
    {

        // EXAMPLES
        //public virtual DbSet<Tenant> Tenants { get; set; }
        //public virtual DbSet<Flat> Flats { get; set; }
        //public virtual DbSet<Rent> Rents { get; set; }
        //

        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<Flat> Flats { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<House> Houses { get; set; }
        public virtual DbSet<PropertyEquipment> PropertyEquipments { get; set; }
        public virtual DbSet<PropertyImage> PropertyImages { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }


        public Flats4usContext() { }

        public Flats4usContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelbuilder.entity<tenant>().hasdata(
            //    new
            //    {
            //        tenantid = 1,
            //        name = "jan",
            //        surname = "kowalski",
            //        addressline1 = "ul. dluga 1",
            //        addressline2 = "",
            //        addressline3 = "",
            //        email = "jan.kowalski@gmail.com",
            //        phonenumber = "123456789"
            //    },
            //    new
            //    {
            //        tenantid = 2,
            //        name = "maciej",
            //        surname = "nowak",
            //        addressline1 = "ul. dluga 45",
            //        addressline2 = "",
            //        addressline3 = "",
            //        email = "maciej.nowak@gmail.com",
            //        phonenumber = "987654321"
            //    }
            //);

            //modelbuilder.entity<flat>().hasdata(
            //    new
            //    {
            //        flatid = 1,
            //        name = "mieszkanie 1",
            //        addressline1 = "ul. dluga 1",
            //        addressline2 = "",
            //        addressline3 = "",
            //        metricarea = 40.0f,
            //        maxnumberofinhabitants = 5
            //    },
            //    new
            //    {
            //        flatid = 2,
            //        name = "mieszkanie 2",
            //        addressline1 = "ul. dluga 45",
            //        addressline2 = "",
            //        addressline3 = "",
            //        metricarea = 50.0f,
            //        maxnumberofinhabitants = 4
            //    }
            //);

            //modelbuilder.entity<rent>().hasdata(
            //    new
            //    {
            //        rentid = 1,
            //        tenantid = 1,
            //        flatid = 2,
            //        startdate = new datetime(2022, 10, 25),
            //        durationinmonths = 10,
            //        pricepermonth = 2000.0f,
            //    },
            //    new
            //    {
            //        rentid = 2,
            //        tenantid = 2,
            //        flatid = 1,
            //        startdate = new datetime(2022, 11, 5),
            //        durationinmonths = 6,
            //        pricepermonth = 2000.0f,
            //    }
            //);

        }
    }
}
