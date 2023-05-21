using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Flats4us.Migrations
{
    /// <inheritdoc />
    public partial class UserAndModerator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Flat = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AccountCreationDate", "City", "Discriminator", "Email", "Flat", "HireDate", "LastLoginDate", "Name", "Number", "Password", "PhoneNumber", "PostalCode", "Street", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 11, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6863), "Warszawa", "Moderator", "maciej.kowalski@gmail.com", 2, new DateTime(2023, 5, 11, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6913), new DateTime(2023, 5, 21, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6911), "Maciej", 47, "mkowalski123", "456736829", "00-000", "Długa", "Kowalski" },
                    { 2, new DateTime(2023, 5, 16, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6916), "Gdańsk", "Moderator", "katarzyna.nowak@gmail.com", 3, new DateTime(2023, 5, 16, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6920), new DateTime(2023, 5, 20, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6918), "Katarzyna", 10, "knowak123", "512345678", "80-000", "Kwiatowa", "Nowak" },
                    { 3, new DateTime(2023, 5, 14, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6922), "Kraków", "Moderator", "adam.kowalczyk@gmail.com", 6, new DateTime(2023, 5, 14, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6925), new DateTime(2023, 5, 19, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6924), "Adam", 20, "akowalczyk789", "601234567", "30-001", "Słoneczna", "Kowalczyk" },
                    { 4, new DateTime(2023, 5, 18, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6928), "Warszawa", "Moderator", "magdalena.wojcik@gmail.com", 2, new DateTime(2023, 5, 18, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6931), new DateTime(2023, 5, 20, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6929), "Magdalena", 15, "mwojcik456", "712345678", "02-000", "Ogrodowa", "Wójcik" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
