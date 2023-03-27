using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flats4us.Migrations
{
    /// <inheritdoc />
    public partial class argument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Intervention",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModeratorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intervention", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intervention_User_ModeratorId",
                        column: x => x.ModeratorId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Argument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OwnerAcceptanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantAcceptanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArgumentStatus = table.Column<int>(type: "int", nullable: false),
                    ModeratorDecisionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    InterventionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Argument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Argument_Intervention_InterventionId",
                        column: x => x.InterventionId,
                        principalTable: "Intervention",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Argument_Offer_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArgumentMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ArgumentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArgumentMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArgumentMessage_Argument_ArgumentId",
                        column: x => x.ArgumentId,
                        principalTable: "Argument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArgumentMessage_User_SenderId",
                        column: x => x.SenderId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Argument_InterventionId",
                table: "Argument",
                column: "InterventionId");

            migrationBuilder.CreateIndex(
                name: "IX_Argument_OfferId",
                table: "Argument",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_ArgumentMessage_ArgumentId",
                table: "ArgumentMessage",
                column: "ArgumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ArgumentMessage_SenderId",
                table: "ArgumentMessage",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Intervention_ModeratorId",
                table: "Intervention",
                column: "ModeratorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArgumentMessage");

            migrationBuilder.DropTable(
                name: "Argument");

            migrationBuilder.DropTable(
                name: "Intervention");
        }
    }
}
