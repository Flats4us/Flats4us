using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Flats4us.Migrations
{
    /// <inheritdoc />
    public partial class seedbazy2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
