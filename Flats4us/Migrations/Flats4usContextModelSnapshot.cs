﻿// <auto-generated />
using System;
using Flats4us.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Flats4us.Migrations
{
    [DbContext(typeof(Flats4usContext))]
    partial class Flats4usContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Flats4us.Entities.Advertisement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("ModeratorId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ModeratorId");

                    b.ToTable("Advertisement");
                });

            modelBuilder.Entity("Flats4us.Entities.Argument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArgumentStatus")
                        .HasColumnType("int");

                    b.Property<int?>("InterventionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModeratorDecisionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OwnerAcceptanceDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TenantAcceptanceDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("InterventionId");

                    b.HasIndex("OfferId");

                    b.ToTable("Argument");
                });

            modelBuilder.Entity("Flats4us.Entities.ArgumentMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ArgumentId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArgumentId");

                    b.HasIndex("SenderId");

                    b.ToTable("ArgumentMessage");
                });

            modelBuilder.Entity("Flats4us.Entities.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Equipment");
                });

            modelBuilder.Entity("Flats4us.Entities.Intervention", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ModeratorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ModeratorId");

                    b.ToTable("Intervention");
                });

            modelBuilder.Entity("Flats4us.Entities.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.ToTable("Meeting");
                });

            modelBuilder.Entity("Flats4us.Entities.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfIntrested")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<string>("Regulations")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RentalPeriod")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("Offer");
                });

            modelBuilder.Entity("Flats4us.Entities.OfferInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.Property<int>("SeekerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.HasIndex("SeekerId");

                    b.ToTable("OfferInterest");
                });

            modelBuilder.Entity("Flats4us.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("WhatFor")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.HasIndex("StudentId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("Flats4us.Entities.PersonOpinion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Check1")
                        .HasColumnType("bit");

                    b.Property<bool>("Check2")
                        .HasColumnType("bit");

                    b.Property<bool>("Check3")
                        .HasColumnType("bit");

                    b.Property<bool>("Check4")
                        .HasColumnType("bit");

                    b.Property<bool>("Check5")
                        .HasColumnType("bit");

                    b.Property<bool>("Check6")
                        .HasColumnType("bit");

                    b.Property<bool>("Check7")
                        .HasColumnType("bit");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PersonOpinion");
                });

            modelBuilder.Entity("Flats4us.Entities.Promotion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("PromotionType")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.ToTable("Promotion");
                });

            modelBuilder.Entity("Flats4us.Entities.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxInhabitants")
                        .HasColumnType("int");

                    b.Property<int>("Surface")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Property");

                    b.HasDiscriminator<string>("Discriminator").IsComplete(true).HasValue("Property");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Flats4us.Entities.PropertyEquipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EquipmentId")
                        .HasColumnType("int");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentId");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertyEquipment");
                });

            modelBuilder.Entity("Flats4us.Entities.PropertyImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("ImageData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertyImage");
                });

            modelBuilder.Entity("Flats4us.Entities.Rent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContractInformations")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LengthInMonths")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.HasIndex("TenantId");

                    b.ToTable("Rent");
                });

            modelBuilder.Entity("Flats4us.Entities.RentOpinion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ComplianceWithOffer")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Decoration")
                        .HasColumnType("int");

                    b.Property<int>("Equipment")
                        .HasColumnType("int");

                    b.Property<int>("Localization")
                        .HasColumnType("int");

                    b.Property<int>("Loudness")
                        .HasColumnType("int");

                    b.Property<int>("Neighbors")
                        .HasColumnType("int");

                    b.Property<int>("ParkingPlace")
                        .HasColumnType("int");

                    b.Property<int>("Tidiness")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RentOpinion");
                });

            modelBuilder.Entity("Flats4us.Entities.StudentMeeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MeetingId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MeetingId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentMeeting");
                });

            modelBuilder.Entity("Flats4us.Entities.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Animal")
                        .HasColumnType("bit");

                    b.Property<int>("Helpfulness")
                        .HasColumnType("int");

                    b.Property<int>("Loudness")
                        .HasColumnType("int");

                    b.Property<int>("MaxNumberOfRoommates")
                        .HasColumnType("int");

                    b.Property<int>("MaxRoommateAge")
                        .HasColumnType("int");

                    b.Property<int>("MinRoommateAge")
                        .HasColumnType("int");

                    b.Property<int>("Party")
                        .HasColumnType("int");

                    b.Property<bool>("Roommate")
                        .HasColumnType("bit");

                    b.Property<int>("RoommateGender")
                        .HasColumnType("int");

                    b.Property<bool>("Smoking")
                        .HasColumnType("bit");

                    b.Property<int>("Sociability")
                        .HasColumnType("int");

                    b.Property<int>("Tidiness")
                        .HasColumnType("int");

                    b.Property<bool>("Vegan")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Survey");
                });

            modelBuilder.Entity("Flats4us.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AccountCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Flats4us.Entities.Flat", b =>
                {
                    b.HasBaseType("Flats4us.Entities.Property");

                    b.Property<int>("NumberOfRooms")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("int")
                        .HasColumnName("NumberOfRooms");

                    b.ToTable("Property");

                    b.HasDiscriminator().HasValue("Flat");
                });

            modelBuilder.Entity("Flats4us.Entities.House", b =>
                {
                    b.HasBaseType("Flats4us.Entities.Property");

                    b.Property<int>("NumberOfFloors")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfRooms")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("int")
                        .HasColumnName("NumberOfRooms");

                    b.Property<int>("ParcelSurface")
                        .HasColumnType("int");

                    b.ToTable("Property");

                    b.HasDiscriminator().HasValue("House");
                });

            modelBuilder.Entity("Flats4us.Entities.Room", b =>
                {
                    b.HasBaseType("Flats4us.Entities.Property");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Property");

                    b.HasDiscriminator().HasValue("Room");
                });

            modelBuilder.Entity("Flats4us.Entities.Moderator", b =>
                {
                    b.HasBaseType("Flats4us.Entities.User");

                    b.Property<int>("Department")
                        .HasColumnType("int");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("datetime2");

                    b.ToTable("User");

                    b.HasDiscriminator().HasValue("Moderator");
                });

            modelBuilder.Entity("Flats4us.Entities.OwnerStudent", b =>
                {
                    b.HasBaseType("Flats4us.Entities.User");

                    b.Property<bool>("ActivityStatus")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DocumentExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DocumentType")
                        .HasColumnType("int");

                    b.Property<byte[]>("Photo")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("VerificationStatus")
                        .HasColumnType("int");

                    b.ToTable("User");

                    b.HasDiscriminator().HasValue("OwnerStudent");
                });

            modelBuilder.Entity("Flats4us.Entities.Owner", b =>
                {
                    b.HasBaseType("Flats4us.Entities.OwnerStudent");

                    b.Property<string>("BankAccount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleDeedPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("User");

                    b.HasDiscriminator().HasValue("Owner");
                });

            modelBuilder.Entity("Flats4us.Entities.Student", b =>
                {
                    b.HasBaseType("Flats4us.Entities.OwnerStudent");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int?>("RentId")
                        .HasColumnType("int");

                    b.Property<string>("StudentNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearOfBirth")
                        .HasColumnType("int");

                    b.HasIndex("RentId");

                    b.ToTable("User");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("Flats4us.Entities.Seeker", b =>
                {
                    b.HasBaseType("Flats4us.Entities.Student");

                    b.ToTable("User");

                    b.HasDiscriminator().HasValue("Seeker");
                });

            modelBuilder.Entity("Flats4us.Entities.Tenant", b =>
                {
                    b.HasBaseType("Flats4us.Entities.Student");

                    b.Property<int>("RoommatesStatus")
                        .HasColumnType("int");

                    b.ToTable("User");

                    b.HasDiscriminator().HasValue("Tenant");
                });

            modelBuilder.Entity("Flats4us.Entities.Advertisement", b =>
                {
                    b.HasOne("Flats4us.Entities.Moderator", "Moderator")
                        .WithMany()
                        .HasForeignKey("ModeratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Moderator");
                });

            modelBuilder.Entity("Flats4us.Entities.Argument", b =>
                {
                    b.HasOne("Flats4us.Entities.Intervention", "Intervention")
                        .WithMany()
                        .HasForeignKey("InterventionId");

                    b.HasOne("Flats4us.Entities.Offer", "Offer")
                        .WithMany()
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Intervention");

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("Flats4us.Entities.ArgumentMessage", b =>
                {
                    b.HasOne("Flats4us.Entities.Argument", null)
                        .WithMany("ArgumentMessages")
                        .HasForeignKey("ArgumentId");

                    b.HasOne("Flats4us.Entities.OwnerStudent", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Flats4us.Entities.Intervention", b =>
                {
                    b.HasOne("Flats4us.Entities.Moderator", null)
                        .WithMany("Interventions")
                        .HasForeignKey("ModeratorId");
                });

            modelBuilder.Entity("Flats4us.Entities.Meeting", b =>
                {
                    b.HasOne("Flats4us.Entities.Offer", "Offer")
                        .WithMany("Meetings")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("Flats4us.Entities.Offer", b =>
                {
                    b.HasOne("Flats4us.Entities.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("Flats4us.Entities.OfferInterest", b =>
                {
                    b.HasOne("Flats4us.Entities.Offer", "Offer")
                        .WithMany()
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Flats4us.Entities.Seeker", "Seeker")
                        .WithMany()
                        .HasForeignKey("SeekerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");

                    b.Navigation("Seeker");
                });

            modelBuilder.Entity("Flats4us.Entities.Payment", b =>
                {
                    b.HasOne("Flats4us.Entities.Offer", "Offer")
                        .WithMany("Payments")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Flats4us.Entities.Student", "Student")
                        .WithMany("Payments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Flats4us.Entities.Promotion", b =>
                {
                    b.HasOne("Flats4us.Entities.Offer", "Offer")
                        .WithMany("Promotions")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("Flats4us.Entities.PropertyEquipment", b =>
                {
                    b.HasOne("Flats4us.Entities.Equipment", "Equipment")
                        .WithMany()
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Flats4us.Entities.Property", "Property")
                        .WithMany("PropertyEquipments")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipment");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("Flats4us.Entities.PropertyImage", b =>
                {
                    b.HasOne("Flats4us.Entities.Property", "Property")
                        .WithMany("PropertyImages")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("Flats4us.Entities.Rent", b =>
                {
                    b.HasOne("Flats4us.Entities.Offer", "Offer")
                        .WithMany("Rentals")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Flats4us.Entities.Tenant", "Tenant")
                        .WithMany("Rents")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("Flats4us.Entities.RentOpinion", b =>
                {
                    b.HasOne("Flats4us.Entities.Rent", "Rent")
                        .WithOne("RentOpinion")
                        .HasForeignKey("Flats4us.Entities.RentOpinion", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rent");
                });

            modelBuilder.Entity("Flats4us.Entities.StudentMeeting", b =>
                {
                    b.HasOne("Flats4us.Entities.Meeting", "Meeting")
                        .WithMany()
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Flats4us.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meeting");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Flats4us.Entities.Survey", b =>
                {
                    b.HasOne("Flats4us.Entities.Student", "Student")
                        .WithOne("Survey")
                        .HasForeignKey("Flats4us.Entities.Survey", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Flats4us.Entities.Student", b =>
                {
                    b.HasOne("Flats4us.Entities.Rent", null)
                        .WithMany("OtherTenants")
                        .HasForeignKey("RentId");
                });

            modelBuilder.Entity("Flats4us.Entities.Argument", b =>
                {
                    b.Navigation("ArgumentMessages");
                });

            modelBuilder.Entity("Flats4us.Entities.Offer", b =>
                {
                    b.Navigation("Meetings");

                    b.Navigation("Payments");

                    b.Navigation("Promotions");

                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("Flats4us.Entities.Property", b =>
                {
                    b.Navigation("PropertyEquipments");

                    b.Navigation("PropertyImages");
                });

            modelBuilder.Entity("Flats4us.Entities.Rent", b =>
                {
                    b.Navigation("OtherTenants");

                    b.Navigation("RentOpinion")
                        .IsRequired();
                });

            modelBuilder.Entity("Flats4us.Entities.Moderator", b =>
                {
                    b.Navigation("Interventions");
                });

            modelBuilder.Entity("Flats4us.Entities.Student", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("Survey")
                        .IsRequired();
                });

            modelBuilder.Entity("Flats4us.Entities.Tenant", b =>
                {
                    b.Navigation("Rents");
                });
#pragma warning restore 612, 618
        }
    }
}
