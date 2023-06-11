using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Flats4us.Migrations
{
    /// <inheritdoc />
    public partial class aad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flat",
                columns: table => new
                {
                    FlatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    AddressLine3 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    MetricArea = table.Column<float>(type: "real", maxLength: 5, nullable: false),
                    MaxNumberOfInhabitants = table.Column<int>(type: "int", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flat", x => x.FlatId);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    TenantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    AddressLine3 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.TenantId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Rent",
                columns: table => new
                {
                    RentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    FlatId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationInMonths = table.Column<int>(type: "int", nullable: false),
                    PricePerMonth = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rent", x => x.RentId);
                    table.ForeignKey(
                        name: "FK_Rent_Flat_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flat",
                        principalColumn: "FlatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rent_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Flat",
                columns: new[] { "FlatId", "AddressLine1", "AddressLine2", "AddressLine3", "MaxNumberOfInhabitants", "MetricArea", "Name" },
                values: new object[,]
                {
                    { 1, "ul. Dluga 1", "", "", 5, 40f, "Mieszkanie 1" },
                    { 2, "ul. Dluga 45", "", "", 4, 50f, "Mieszkanie 2" }
                });

            migrationBuilder.InsertData(
                table: "Tenant",
                columns: new[] { "TenantId", "AddressLine1", "AddressLine2", "AddressLine3", "Email", "Name", "PhoneNumber", "Surname" },
                values: new object[,]
                {
                    { 1, "ul. Dluga 1", "", "", "jan.kowalski@gmail.com", "Jan", "123456789", "Kowalski" },
                    { 2, "ul. Dluga 45", "", "", "maciej.nowak@gmail.com", "Maciej", "987654321", "Nowak" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, "9uaPpbDg;0B:9540oyr,%\\\"Y~6\"<P(RkX`dY)S?NlUPTtE!Q6f", "Dominik" },
                    { 2, "9uaPpbDg;0B:9540oyr,%\\\"Y~6\"<P(RkX`dY)S?NlUPTtE!Q6f", "testuser" }
                });

            migrationBuilder.InsertData(
                table: "Rent",
                columns: new[] { "RentId", "DurationInMonths", "FlatId", "PricePerMonth", "StartDate", "TenantId" },
                values: new object[,]
                {
                    { 1, 10, 2, 2000f, new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 6, 1, 2000f, new DateTime(2022, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rent_FlatId",
                table: "Rent",
                column: "FlatId");

            migrationBuilder.CreateIndex(
                name: "IX_Rent_TenantId",
                table: "Rent",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rent");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Flat");

            migrationBuilder.DropTable(
                name: "Tenant");
        }
    }
}
