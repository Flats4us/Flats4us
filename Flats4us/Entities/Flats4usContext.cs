﻿using Flats4us.Helpers.Enums;
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
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<GroupChat> GroupChats { get; set; }
        public DbSet<UserGroupChat> UserGroupChats { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<Flat> Flats { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Matcher> Matcher { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferInterest> OfferInterests { get; set; }
        public DbSet<OfferPromotion> OfferPromotions { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<OwnerStudent> OwnerStudents { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<RentOpinion> RentOpinions { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<SurveyOwnerOffer> OwnerOfferSurveys { get; set; }
        public DbSet<SurveyStudent> StudentSurveys { get; set; }
        public DbSet<TechnicalProblem> TechnicalProblems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOpinion> UserOpinions { get; set; }

        public Flats4usContext() { }

        public Flats4usContext(DbContextOptions options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notification>()
                .HasOne(x => x.User)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .HasOne(x => x.User1)
                .WithMany()
                .HasForeignKey(x => x.User1Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .HasOne(x => x.User2)
                .WithMany()
                .HasForeignKey(x => x.User2Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .HasIndex(x => new { x.User1Id, x.User2Id })
                .IsUnique();

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
                .HasForeignKey<SurveyOwnerOffer>(soo => soo.OfferId)
                .OnDelete(DeleteBehavior.Cascade);

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
                .OnDelete(DeleteBehavior.Cascade);

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

            modelBuilder.Entity<TechnicalProblem>()
                .HasOne(x => x.User)
                .WithMany(x => x.TechnicalProblems)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<UserGroupChat>()
                .HasKey(ugc => new { ugc.UserId, ugc.GroupChatId });

            modelBuilder.Entity<Rent>()
                .HasOne(x => x.Student)
                .WithMany(x => x.Rents)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Rent>()
                .HasOne(x => x.Student)
                .WithMany(x => x.Rents)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rent>()
                .HasMany<Student>(x => x.OtherStudents)
                .WithMany(x => x.RoommateInRents);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Rent)
                .WithOne(r => r.Offer)
                .HasForeignKey<Rent>(r => r.OfferId);

            modelBuilder.Entity<UserOpinion>()
                .HasOne(x => x.SourceUser)
                .WithMany(x => x.IssuedUserOpinions)
                .HasForeignKey(x => x.SourceUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserGroupChat>()
                .HasKey(ugc => new { ugc.UserId, ugc.GroupChatId });

            modelBuilder.Entity<UserGroupChat>()
                .HasOne(ugc => ugc.User)
                .WithMany(u => u.UserGroupChats)
                .HasForeignKey(ugc => ugc.UserId);

            modelBuilder.Entity<UserGroupChat>()
                .HasOne(ugc => ugc.GroupChat)
                .WithMany(gc => gc.UserGroupChats)
                .HasForeignKey(ugc => ugc.GroupChatId);

            modelBuilder.Entity<UserOpinion>()
                .HasOne(x => x.TargetUser)
                .WithMany(x => x.ReceivedUserOpinions)
                .HasForeignKey(x => x.TargetUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RentOpinion>()
               .HasOne(x => x.Property)
               .WithMany(x => x.RentOpinions)
               .HasForeignKey(x => x.PropertyId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RentOpinion>()
                .HasOne(ro => ro.Student) 
                .WithMany(s => s.IssuedRentOpinions)  
                .HasForeignKey(ro => ro.StudentId)  
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Rent)
                .WithMany(r => r.Payments)
                .HasForeignKey(p => p.RentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OfferInterest>()
                .HasOne(oi => oi.Offer)
                .WithMany(o => o.OfferInterests)
                .HasForeignKey(oi => oi.OfferId);

            modelBuilder.Entity<OfferInterest>()
                .HasOne(oi => oi.Student)
                .WithMany(o => o.OfferInterests)
                .HasForeignKey(oi => oi.StudentId);

            modelBuilder.Entity<ArgumentIntervention>()
               .HasOne(x => x.Argument)
               .WithMany(x => x.ArgumentInterventions)
               .HasForeignKey(x => x.ArgumentId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ArgumentIntervention>()
                .HasOne(x => x.Moderator)
                .WithMany(x => x.ArgumentInterventions)
                .HasForeignKey(x => x.ModeratorId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Argument>()
            //.HasOne(a => a.GroupChat)
            //.WithOne(g => g.Argument)
            //.HasForeignKey<Argument>(a => a.GroupChatId)
            //.OnDelete(DeleteBehavior.Cascade);
        }
    }
}
