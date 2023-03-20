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

            modelBuilder.Entity("Flats4us.Entities.Flat", b =>
                {
                    b.Property<int>("FlatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlatId"));

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("AddressLine2")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("AddressLine3")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("MaxNumberOfInhabitants")
                        .HasMaxLength(5)
                        .HasColumnType("int");

                    b.Property<float>("MetricArea")
                        .HasMaxLength(5)
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("FlatId");

                    b.ToTable("Flat");

                    b.HasData(
                        new
                        {
                            FlatId = 1,
                            AddressLine1 = "ul. Dluga 1",
                            AddressLine2 = "",
                            AddressLine3 = "",
                            MaxNumberOfInhabitants = 5,
                            MetricArea = 40f,
                            Name = "Mieszkanie 1"
                        },
                        new
                        {
                            FlatId = 2,
                            AddressLine1 = "ul. Dluga 45",
                            AddressLine2 = "",
                            AddressLine3 = "",
                            MaxNumberOfInhabitants = 4,
                            MetricArea = 50f,
                            Name = "Mieszkanie 2"
                        });
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

                    b.Property<int>("MaxInhabitants")
                        .HasColumnType("int");

                    b.Property<int>("Surface")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Property");
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
                    b.Property<int>("RentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RentId"));

                    b.Property<int>("DurationInMonths")
                        .HasColumnType("int");

                    b.Property<int>("FlatId")
                        .HasColumnType("int");

                    b.Property<float>("PricePerMonth")
                        .HasColumnType("real");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("RentId");

                    b.HasIndex("FlatId");

                    b.HasIndex("TenantId");

                    b.ToTable("Rent");

                    b.HasData(
                        new
                        {
                            RentId = 1,
                            DurationInMonths = 10,
                            FlatId = 2,
                            PricePerMonth = 2000f,
                            StartDate = new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TenantId = 1
                        },
                        new
                        {
                            RentId = 2,
                            DurationInMonths = 6,
                            FlatId = 1,
                            PricePerMonth = 2000f,
                            StartDate = new DateTime(2022, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TenantId = 2
                        });
                });

            modelBuilder.Entity("Flats4us.Entities.Tenant", b =>
                {
                    b.Property<int>("TenantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TenantId"));

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("AddressLine2")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("AddressLine3")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TenantId");

                    b.ToTable("Tenant");

                    b.HasData(
                        new
                        {
                            TenantId = 1,
                            AddressLine1 = "ul. Dluga 1",
                            AddressLine2 = "",
                            AddressLine3 = "",
                            Email = "jan.kowalski@gmail.com",
                            Name = "Jan",
                            PhoneNumber = "123456789",
                            Surname = "Kowalski"
                        },
                        new
                        {
                            TenantId = 2,
                            AddressLine1 = "ul. Dluga 45",
                            AddressLine2 = "",
                            AddressLine3 = "",
                            Email = "maciej.nowak@gmail.com",
                            Name = "Maciej",
                            PhoneNumber = "987654321",
                            Surname = "Nowak"
                        });
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
                    b.HasOne("Flats4us.Entities.Flat", "Flat")
                        .WithMany("Rents")
                        .HasForeignKey("FlatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Flats4us.Entities.Tenant", "Tenant")
                        .WithMany("Rents")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flat");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("Flats4us.Entities.Flat", b =>
                {
                    b.Navigation("Rents");
                });

            modelBuilder.Entity("Flats4us.Entities.Property", b =>
                {
                    b.Navigation("PropertyEquipments");

                    b.Navigation("PropertyImages");
                });

            modelBuilder.Entity("Flats4us.Entities.Tenant", b =>
                {
                    b.Navigation("Rents");
                });
#pragma warning restore 612, 618
        }
    }
}
