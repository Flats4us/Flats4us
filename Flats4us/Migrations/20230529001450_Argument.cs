using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flats4us.Migrations
{
    /// <inheritdoc />
    public partial class Argument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Argument",
                columns: table => new
                {
                    ArgumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentAcceptanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OwnerAcceptanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArgumentStatus = table.Column<int>(type: "int", nullable: false),
                    MederatorDecisionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    StudentUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Argument", x => x.ArgumentId);
                    table.ForeignKey(
                        name: "FK_Argument_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Argument_Users_StudentUserId",
                        column: x => x.StudentUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArgumentMessage",
                columns: table => new
                {
                    ArgumentMessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sender = table.Column<int>(type: "int", nullable: false),
                    ArgumentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArgumentMessage", x => x.ArgumentMessageId);
                    table.ForeignKey(
                        name: "FK_ArgumentMessage_Argument_ArgumentId",
                        column: x => x.ArgumentId,
                        principalTable: "Argument",
                        principalColumn: "ArgumentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Argument_OfferId",
                table: "Argument",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Argument_StudentUserId",
                table: "Argument",
                column: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArgumentMessage_ArgumentId",
                table: "ArgumentMessage",
                column: "ArgumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArgumentMessage");

            migrationBuilder.DropTable(
                name: "Argument");
        }
    }
}
