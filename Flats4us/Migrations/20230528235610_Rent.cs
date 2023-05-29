using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flats4us.Migrations
{
    /// <inheritdoc />
    public partial class Rent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rents",
                columns: table => new
                {
                    RentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RentPeriod = table.Column<int>(type: "int", nullable: false),
                    ContractInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OffersOfferId = table.Column<int>(type: "int", nullable: false),
                    TenantUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.RentId);
                    table.ForeignKey(
                        name: "FK_Rents_Offers_OffersOfferId",
                        column: x => x.OffersOfferId,
                        principalTable: "Offers",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rents_Users_TenantUserId",
                        column: x => x.TenantUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentOpinions",
                columns: table => new
                {
                    OpinionRentId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<int>(type: "int", nullable: false),
                    Neighbors = table.Column<int>(type: "int", nullable: false),
                    Equipment = table.Column<int>(type: "int", nullable: false),
                    ParkingSpace = table.Column<int>(type: "int", nullable: false),
                    Tidiness = table.Column<int>(type: "int", nullable: false),
                    Decoration = table.Column<int>(type: "int", nullable: false),
                    Noisiness = table.Column<int>(type: "int", nullable: false),
                    ComplianceWithOffer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentOpinions", x => x.OpinionRentId);
                    table.ForeignKey(
                        name: "FK_RentOpinions_Rents_OpinionRentId",
                        column: x => x.OpinionRentId,
                        principalTable: "Rents",
                        principalColumn: "RentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rents_OffersOfferId",
                table: "Rents",
                column: "OffersOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_TenantUserId",
                table: "Rents",
                column: "TenantUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentOpinions");

            migrationBuilder.DropTable(
                name: "Rents");
        }
    }
}
