using Flats4us.Helpers.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Flats4us.Entities
{
    public class Flats4usContext : DbContext
    {
        public DbSet<Argument> Arguments { get; set; }
        public DbSet<ArgumentIntervention> ArgumentInterventions { get; set; }
        public DbSet<ArgumentMessage> ArgumentMessages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Flat> Flats { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Matcher> Matcher { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferInterest> OfferInterests { get; set; }
        public DbSet<OfferPromotion> OfferPromotions { get; set; }
        public DbSet<OpinionOwnerStudent> OwnerStudentOpinions { get; set; }
        public DbSet<OpinionRent> RentOpinions { get; set; }
        public DbSet<OpinionStudentOwner> StudentOwnerOpinions { get; set; }
        public DbSet<OpinionStudentStudent> StudentStudentOpinions { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<OwnerStudent> OwnerStudents { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<SurveyOwnerOffer> OwnerOfferSurveys { get; set; }
        public DbSet<SurveyStudent> StudentSurveys { get; set; }
        public DbSet<TechnicalProblem> TechnicalProblems { get; set; }
        public DbSet<User> Users { get; set; }

        public Flats4usContext() { }

        public Flats4usContext(DbContextOptions options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OpinionStudentStudent>()
                .HasOne(x => x.Evaluated)
                .WithMany(x => x.ReceivedStudentStudentOpinions)
                .HasForeignKey(x => x.EvaluatedId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OpinionStudentStudent>()
                .HasOne(x => x.Evaluator)
                .WithMany(x => x.IssuedStudentStudentOpinions)
                .HasForeignKey(x => x.EvaluatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OpinionOwnerStudent>()
                .HasOne(x => x.Evaluated)
                .WithMany(x => x.ReceivedOwnertStudentOpinions)
                .HasForeignKey(x => x.EvaluatedId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OpinionOwnerStudent>()
                .HasOne(x => x.Evaluator)
                .WithMany(x => x.IssuedOwnerStudentOpinions)
                .HasForeignKey(x => x.EvaluatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OpinionStudentOwner>()
                .HasOne(x => x.Evaluator)
                .WithMany(x => x.IssuedStudentOwnerOpinions)
                .HasForeignKey(x => x.EvaluatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OpinionStudentOwner>()
                .HasOne(x => x.Evaluated)
                .WithMany(x => x.ReceivedStudentOwnerOpinions)
                .HasForeignKey(x => x.EvaluatedId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.Chats)
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .HasOne(x => x.Student)
                .WithMany(x => x.Chats)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Property>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Offer>()
                .HasOne(x => x.Property)
                .WithMany(x => x.Offers)
                .HasForeignKey(x => x.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.SurveyOwnerOffer)
                .WithOne(soo => soo.Offer)
                .HasForeignKey<SurveyOwnerOffer>(soo => soo.OfferId);

            modelBuilder.Entity<Student>()
                .HasOne(o => o.SurveyStudent)
                .WithOne(soo => soo.Student)
                .HasForeignKey<SurveyStudent>(soo => soo.StudentId);

            modelBuilder.Entity<Meeting>()
                .HasOne(x => x.Offer)
                .WithMany(x => x.Meetings)
                .HasForeignKey(x => x.OfferId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Meeting>()
                .HasOne(x => x.Student)
                .WithMany(x => x.Meetings)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OfferPromotion>()
                .HasOne(x => x.Offer)
                .WithMany(x => x.OfferPromotions)
                .HasForeignKey(x => x.OfferId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Matcher>()
                .HasOne(x => x.Student1)
                .WithMany()
                .HasForeignKey(x => x.Student1Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Matcher>()
                .HasOne(x => x.Student2)
                .WithMany()
                .HasForeignKey(x => x.Student2Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Matcher>()
                .HasIndex(x => new { x.Student1Id, x.Student2Id })
                .IsUnique();

            modelBuilder.Entity<Matcher>()
                .ToTable(builder =>
                {
                    builder.HasCheckConstraint("CK_Matcher_StudentIds", "Student1Id < Student2Id");
                });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<TechnicalProblem>()
                .HasOne(x => x.User)
                .WithMany(x => x.TechnicalProblems)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
