using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Flats4us.Migrations
{
    /// <inheritdoc />
    public partial class _20230504done : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Equipment",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Laptop" },
                    { 2, "Projector" },
                    { 3, "Microphone" }
                });

            migrationBuilder.InsertData(
                table: "Intervention",
                columns: new[] { "Id", "Date", "Description", "ModeratorId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intervention 1", null },
                    { 2, new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intervention 2", null },
                    { 3, new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intervention 3", null }
                });

            migrationBuilder.InsertData(
                table: "Property",
                columns: new[] { "Id", "Address", "Discriminator", "MaxInhabitants", "NumberOfRooms", "Surface" },
                values: new object[,]
                {
                    { 1, "ul. Wiejska 1, Warszawa", "Flat", 2, 1, 70 },
                    { 2, "ul. Wrocławska 12, Kraków", "Flat", 4, 1, 120 },
                    { 3, "ul. Kościuszki 50, Gdańsk", "Flat", 3, 1, 90 }
                });

            migrationBuilder.InsertData(
                table: "Property",
                columns: new[] { "Id", "Address", "Discriminator", "MaxInhabitants", "NumberOfFloors", "NumberOfRooms", "ParcelSurface", "Surface" },
                values: new object[,]
                {
                    { 4, "ul. Mickiewicza 20, Kraków", "House", 6, 2, 5, 500, 150 },
                    { 5, "ul. Długa 15, Gdańsk", "House", 4, 1, 4, 300, 100 },
                    { 6, "ul. Jagiellońska 10, Warszawa", "House", 8, 3, 6, 700, 200 }
                });

            migrationBuilder.InsertData(
                table: "Property",
                columns: new[] { "Id", "Address", "Discriminator", "MaxInhabitants", "Surface" },
                values: new object[,]
                {
                    { 1111, "ul. Wiejska 1, Warszawa", "Property", 2, 70 },
                    { 2111, "ul. Wrocławska 12, Kraków", "Property", 4, 120 },
                    { 3111, "ul. Kościuszki 50, Gdańsk", "Property", 3, 90 }
                });

            migrationBuilder.InsertData(
                table: "Property",
                columns: new[] { "Id", "Address", "Discriminator", "MaxInhabitants", "Name", "Surface" },
                values: new object[,]
                {
                    { 821111, "ul. Wiejska 1, Warszawa", "Room", 2, "Największy", 70 },
                    { 821112, "ul. Wrocławska 12, Kraków", "Room", 4, "Najmniejszy", 120 },
                    { 821113, "ul. Kościuszki 50, Gdańsk", "Room", 3, "Najśredniejszy", 90 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccountCreationDate", "Address", "City", "Department", "Discriminator", "Email", "HireDate", "LastLoginDate", "Name", "Password", "PhoneNumber", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "123 Main St", "New York", 0, "Moderator", "john.doe@example.com", new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", "password123", "555-1234", "Doe" },
                    { 2, new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "456 Park Ave", "Los Angeles", 1, "Moderator", "jane.smith@example.com", new DateTime(2023, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane", "password456", "555-5678", "Smith" },
                    { 3, new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "789 5th Ave", "Chicago", 0, "Moderator", "bob.johnson@example.com", new DateTime(2023, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bob", "password789", "555-9012", "Johnson" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccountCreationDate", "ActivityStatus", "Address", "Age", "City", "Discriminator", "DocumentExpireDate", "DocumentPath", "DocumentType", "Email", "LastLoginDate", "Name", "Password", "PhoneNumber", "PhotoPath", "RentId", "RoommatesStatus", "StudentNumber", "Surname", "VerificationStatus", "YearOfBirth" },
                values: new object[,]
                {
                    { 4, new DateTime(2023, 4, 4, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3425), true, "123 Main St", 23, "New York", "Tenant", new DateTime(2024, 5, 4, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3427), "/path/to/document.pdf", 1, "alice.smith@example.com", new DateTime(2023, 5, 3, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3426), "Alice", "password123", "555-1234", "/path/to/photo.jpg", null, 1, "123456", "Smith", 0, 2000 },
                    { 5, new DateTime(2023, 4, 4, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3429), true, "456 Oak St", 22, "San Francisco", "Tenant", new DateTime(2024, 5, 4, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3431), "/path/to/document.pdf", 0, "bob.johnson@example.com", new DateTime(2023, 5, 3, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3430), "Bob", "password456", "555-5678", "/path/to/photo.jpg", null, 0, "654321", "Johnson", 0, 2001 },
                    { 6, new DateTime(2023, 4, 4, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3432), true, "789 Maple Ave", 24, "Chicago", "Tenant", new DateTime(2024, 5, 4, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3434), "/path/to/document.pdf", 2, "charlie.brown@example.com", new DateTime(2023, 5, 3, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3433), "Charlie", "password789", "555-9012", "/path/to/photo.jpg", null, 0, "789012", "Brown", 1, 1999 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccountCreationDate", "ActivityStatus", "Address", "Age", "City", "Discriminator", "DocumentExpireDate", "DocumentPath", "DocumentType", "Email", "LastLoginDate", "Name", "Password", "PhoneNumber", "PhotoPath", "RentId", "StudentNumber", "Surname", "VerificationStatus", "YearOfBirth" },
                values: new object[,]
                {
                    { 7, new DateTime(2023, 4, 4, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3364), true, "123 Main St", 23, "New York", "Seeker", new DateTime(2024, 5, 4, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3365), "/path/to/document.pdf", 1, "alice.smith@example.com", new DateTime(2023, 5, 3, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3365), "Alice", "password123", "555-1234", "/path/to/photo.jpg", null, "123456", "Smith", 0, 2000 },
                    { 8, new DateTime(2023, 3, 5, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3370), true, "456 Elm St", 28, "Los Angeles", "Seeker", new DateTime(2025, 5, 4, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3372), "/path/to/document2.pdf", 1, "bob.jones@example.com", new DateTime(2023, 4, 24, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3371), "Bob", "password456", "555-5678", "/path/to/photo2.jpg", null, "789012", "Jones", 0, 1995 },
                    { 9, new DateTime(2023, 4, 19, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3373), true, "789 Oak St", 25, "Chicago", "Seeker", new DateTime(2028, 5, 4, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3375), "/path/to/document3.pdf", 2, "carol.johnson@example.com", new DateTime(2023, 5, 2, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3374), "Carol", "password789", "555-9012", "/path/to/photo3.jpg", null, "345678", "Johnson", 0, 1998 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccountCreationDate", "ActivityStatus", "Address", "BankAccount", "City", "Discriminator", "DocumentExpireDate", "DocumentPath", "DocumentType", "Email", "LastLoginDate", "Name", "Password", "PhoneNumber", "PhotoPath", "Surname", "TitleDeedPath", "VerificationStatus" },
                values: new object[,]
                {
                    { 10, new DateTime(2023, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "ul. Kolorowa 12", "12345678901234567890123456", "Warszawa", "Owner", new DateTime(2028, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "/documents/id_card.pdf", 0, "jan.kowalski@example.com", new DateTime(2023, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jan", "password123", "123456789", "/photos/jan_kowalski.jpg", "Kowalski", "/documents/title_deed.pdf", 0 },
                    { 11, new DateTime(2023, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ul. Zielona 34", "09876543210987654321098765", "Kraków", "Owner", new DateTime(2027, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "/documents/passport.pdf", 2, "anna.nowak@example.com", new DateTime(2023, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anna", "password456", "987654321", "/photos/anna_nowak.jpg", "Nowak", "/documents/title_deed.pdf", 1 },
                    { 12, new DateTime(2023, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "ul. Czerwona 56", "11122233344455566677788899", "Wrocław", "Owner", new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "/documents/student_card.pdf", 1, "piotr.wojcik@example.com", new DateTime(2023, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Piotr", "password789", "555666777", "/photos/piotr_wojcik.jpg", "Wójcik", "/documents/title_deed.pdf", 0 }
                });

            migrationBuilder.InsertData(
                table: "Advertisement",
                columns: new[] { "Id", "EndDate", "Image", "ModeratorId", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 18, 52, 86, 120 }, 1, 100 },
                    { 2, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 18, 52, 86, 120 }, 2, 250 },
                    { 3, new DateTime(2023, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 18, 52, 86, 120 }, 3, 470 }
                });

            migrationBuilder.InsertData(
                table: "ArgumentMessage",
                columns: new[] { "Id", "ArgumentId", "Content", "Date", "SenderId" },
                values: new object[,]
                {
                    { 1, null, "First argument message", new DateTime(2023, 5, 4, 19, 36, 44, 410, DateTimeKind.Local).AddTicks(2749), 1 },
                    { 2, null, "Second argument message", new DateTime(2023, 5, 3, 19, 36, 44, 410, DateTimeKind.Local).AddTicks(2803), 2 },
                    { 3, null, "Third argument message", new DateTime(2023, 5, 2, 19, 36, 44, 410, DateTimeKind.Local).AddTicks(2809), 3 }
                });

            migrationBuilder.InsertData(
                table: "Offer",
                columns: new[] { "Id", "Date", "Description", "NumberOfIntrested", "Price", "PropertyId", "Regulations", "RentalPeriod" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 4, 19, 36, 44, 410, DateTimeKind.Local).AddTicks(3000), "First offer", 5, 1000, 1, "First offer regulations", 12 },
                    { 2, new DateTime(2023, 5, 3, 19, 36, 44, 410, DateTimeKind.Local).AddTicks(3005), "Second offer", 10, 2000, 2, "Second offer regulations", 6 },
                    { 3, new DateTime(2023, 5, 2, 19, 36, 44, 410, DateTimeKind.Local).AddTicks(3008), "Third offer", 3, 1500, 3, "Third offer regulations", 9 }
                });

            migrationBuilder.InsertData(
                table: "PropertyEquipment",
                columns: new[] { "Id", "EquipmentId", "PropertyId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "PropertyImage",
                columns: new[] { "Id", "ImagePath", "PropertyId", "Title" },
                values: new object[,]
                {
                    { 1, "images/livingroom.jpg", 1111, "Living room" },
                    { 2, "images/kitchen.jpg", 2111, "Kitchen" },
                    { 3, "images/bedroom.jpg", 3111, "Bedroom" }
                });

            migrationBuilder.InsertData(
                table: "Survey",
                columns: new[] { "Id", "Animal", "Helpfulness", "Loudness", "MaxNumberOfRoommates", "MaxRoommateAge", "MinRoommateAge", "Party", "Roommate", "RoommateGender", "Smoking", "Sociability", "Tidiness", "Vegan" },
                values: new object[,]
                {
                    { 1, false, 6, 4, 2, 25, 20, 8, true, 1, false, 7, 9, true },
                    { 2, false, 9, 7, 1, 30, 22, 5, false, 0, true, 8, 6, false },
                    { 3, true, 7, 3, 3, 25, 18, 9, true, 2, false, 6, 8, false }
                });

            migrationBuilder.InsertData(
                table: "Argument",
                columns: new[] { "Id", "ArgumentStatus", "InterventionId", "ModeratorDecisionDate", "OfferId", "OwnerAcceptanceDate", "StartDate", "TenantAcceptanceDate" },
                values: new object[,]
                {
                    { 1, 0, 1, new DateTime(2023, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, 2, new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2023, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 3, new DateTime(2023, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2023, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Meeting",
                columns: new[] { "Id", "Address", "Date", "OfferId", "Reason" },
                values: new object[,]
                {
                    { 1, "First meeting address", new DateTime(2023, 5, 4, 19, 36, 44, 410, DateTimeKind.Local).AddTicks(2915), 1, "First meeting reason" },
                    { 2, "Second meeting address", new DateTime(2023, 5, 5, 19, 36, 44, 410, DateTimeKind.Local).AddTicks(2920), 2, "Second meeting reason" },
                    { 3, "Third meeting address", new DateTime(2023, 5, 6, 19, 36, 44, 410, DateTimeKind.Local).AddTicks(2924), 3, "Third meeting reason" }
                });

            migrationBuilder.InsertData(
                table: "OfferInterest",
                columns: new[] { "Id", "Date", "OfferId", "SeekerId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 29, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3044), 1, 1 },
                    { 2, new DateTime(2023, 5, 2, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3046), 1, 2 },
                    { 3, new DateTime(2023, 5, 3, 17, 36, 44, 410, DateTimeKind.Utc).AddTicks(3048), 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Payment",
                columns: new[] { "Id", "OfferId", "Price", "StudentId", "WhatFor" },
                values: new object[,]
                {
                    { 1, 1, 1500, 4, 0 },
                    { 2, 2, 2000, 5, 1 },
                    { 3, 1, 1200, 4, 0 },
                    { 4, 2, 500, 5, 2 },
                    { 5, 3, 1000, 6, 0 },
                    { 6, 3, 1500, 6, 1 },
                    { 7, 1, 1000, 4, 0 },
                    { 8, 2, 500, 5, 1 },
                    { 9, 1, 250, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "Promotion",
                columns: new[] { "Id", "EndDate", "OfferId", "Price", "PromotionType", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 50, 0, new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 75, 1, new DateTime(2023, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2023, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 100, 0, new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 85, 1, new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2023, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 60, 0, new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2023, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 90, 1, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2023, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 120, 0, new DateTime(2023, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2023, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 70, 1, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 95, 0, new DateTime(2023, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2023, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 80, 1, new DateTime(2023, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Rent",
                columns: new[] { "Id", "ContractInformations", "LengthInMonths", "OfferId", "StartDate", "TenantId" },
                values: new object[,]
                {
                    { 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", 12, 1, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", 6, 2, new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.", 3, 3, new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.", 9, 1, new DateTime(2022, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", 12, 2, new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 6, "Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit.", 6, 3, new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 7, "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium.", 3, 1, new DateTime(2022, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 8, "Totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.", 9, 2, new DateTime(2022, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 9, "Totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.", 9, 3, new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 10, "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.", 9, 1, new DateTime(2022, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "RentOpinion",
                columns: new[] { "Id", "ComplianceWithOffer", "Date", "Decoration", "Equipment", "Localization", "Loudness", "Neighbors", "ParkingPlace", "Tidiness" },
                values: new object[,]
                {
                    { 1, 8, new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 9, 8, 4, 7, 6, 9 },
                    { 2, 9, new DateTime(2022, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 8, 7, 5, 6, 5, 8 },
                    { 3, 7, new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 7, 9, 6, 8, 8, 7 },
                    { 4, 8, new DateTime(2022, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 6, 6, 7, 7, 4, 6 },
                    { 5, 8, new DateTime(2022, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 7, 8, 3, 9, 8, 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Advertisement",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Advertisement",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Advertisement",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Argument",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Argument",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Argument",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ArgumentMessage",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ArgumentMessage",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ArgumentMessage",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Meeting",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Meeting",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Meeting",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OfferInterest",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OfferInterest",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OfferInterest",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Promotion",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Promotion",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Promotion",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Promotion",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Promotion",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Promotion",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Promotion",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Promotion",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Promotion",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Promotion",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 821111);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 821112);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 821113);

            migrationBuilder.DeleteData(
                table: "PropertyEquipment",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PropertyEquipment",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PropertyEquipment",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PropertyImage",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PropertyImage",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PropertyImage",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rent",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Rent",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Rent",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Rent",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Rent",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "RentOpinion",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RentOpinion",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RentOpinion",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RentOpinion",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RentOpinion",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Survey",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Intervention",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Intervention",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Intervention",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 1111);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 2111);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 3111);

            migrationBuilder.DeleteData(
                table: "Rent",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rent",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rent",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rent",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rent",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Offer",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Offer",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Offer",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccountCreationDate", "ActivityStatus", "Address", "Age", "City", "Discriminator", "DocumentExpireDate", "DocumentPath", "DocumentType", "Email", "LastLoginDate", "Name", "Password", "PhoneNumber", "PhotoPath", "RentId", "RoommatesStatus", "StudentNumber", "Surname", "VerificationStatus", "YearOfBirth" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 2, 13, 35, 7, 562, DateTimeKind.Utc).AddTicks(4593), true, "123 Main St", 23, "New York", "Tenant", new DateTime(2024, 5, 2, 13, 35, 7, 562, DateTimeKind.Utc).AddTicks(4604), "/path/to/document.pdf", 1, "alice.smith@example.com", new DateTime(2023, 5, 1, 13, 35, 7, 562, DateTimeKind.Utc).AddTicks(4603), "Alice", "password123", "555-1234", "/path/to/photo.jpg", null, 1, "123456", "Smith", 0, 2000 },
                    { 2, new DateTime(2023, 4, 2, 13, 35, 7, 562, DateTimeKind.Utc).AddTicks(4615), true, "456 Oak St", 22, "San Francisco", "Tenant", new DateTime(2024, 5, 2, 13, 35, 7, 562, DateTimeKind.Utc).AddTicks(4618), "/path/to/document.pdf", 0, "bob.johnson@example.com", new DateTime(2023, 5, 1, 13, 35, 7, 562, DateTimeKind.Utc).AddTicks(4617), "Bob", "password456", "555-5678", "/path/to/photo.jpg", null, 0, "654321", "Johnson", 0, 2001 },
                    { 3, new DateTime(2023, 4, 2, 13, 35, 7, 562, DateTimeKind.Utc).AddTicks(4623), true, "789 Maple Ave", 24, "Chicago", "Tenant", new DateTime(2024, 5, 2, 13, 35, 7, 562, DateTimeKind.Utc).AddTicks(4625), "/path/to/document.pdf", 2, "charlie.brown@example.com", new DateTime(2023, 5, 1, 13, 35, 7, 562, DateTimeKind.Utc).AddTicks(4624), "Charlie", "password789", "555-9012", "/path/to/photo.jpg", null, 0, "789012", "Brown", 1, 1999 }
                });
        }
    }
}
