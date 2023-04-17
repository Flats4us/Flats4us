using Microsoft.EntityFrameworkCore;

namespace Flats4us.Entities
{
    public class Flats4usContext : DbContext
    {
        public virtual DbSet<Advertisement> Advertisements { get; set; }
        public virtual DbSet<Argument> Arguments { get; set; }
        public virtual DbSet<ArgumentMessage> ArgumentMessages { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<Flat> Flats { get; set; }
        public virtual DbSet<House> Houses { get; set; }
        public virtual DbSet<Intervention> Interventions { get; set; }
        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<Moderator> Moderators { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<OfferInterest> OfferInterests { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<OwnerOpinion> OwnerOpinions { get; set; }
        public virtual DbSet<OwnerStudent> OwnerStudents { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<PropertyEquipment> PropertyEquipments { get; set; }
        public virtual DbSet<PropertyImage> PropertyImages { get; set; }
        public virtual DbSet<Rent> Rents { get; set; }
        public virtual DbSet<RentOpinion> RentOpinions { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Seeker> Seekers { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentMeeting> StudentMeetings { get; set; }
        public virtual DbSet<StudentOpinion> StudentOpinions { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public Flats4usContext() { }

        public Flats4usContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>()
                .HasDiscriminator()
                .IsComplete();

            modelBuilder.Entity<StudentOpinion>()
                .HasOne(x => x.Evaluated)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentOpinion>()
                .HasOne(x => x.Evaluator)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OwnerOpinion>()
                .HasOne(x => x.Evaluated)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OwnerOpinion>()
                .HasOne(x => x.Evaluator)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

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
