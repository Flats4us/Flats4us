using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flats4us.Migrations
{
    /// <inheritdoc />
    public partial class ArgumentIntervention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Argument_Offers_OfferId",
                table: "Argument");

            migrationBuilder.DropForeignKey(
                name: "FK_Argument_Users_StudentUserId",
                table: "Argument");

            migrationBuilder.DropForeignKey(
                name: "FK_ArgumentMessage_Argument_ArgumentId",
                table: "ArgumentMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArgumentMessage",
                table: "ArgumentMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Argument",
                table: "Argument");

            migrationBuilder.RenameTable(
                name: "ArgumentMessage",
                newName: "ArgumentMessages");

            migrationBuilder.RenameTable(
                name: "Argument",
                newName: "Arguments");

            migrationBuilder.RenameIndex(
                name: "IX_ArgumentMessage_ArgumentId",
                table: "ArgumentMessages",
                newName: "IX_ArgumentMessages_ArgumentId");

            migrationBuilder.RenameIndex(
                name: "IX_Argument_StudentUserId",
                table: "Arguments",
                newName: "IX_Arguments_StudentUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Argument_OfferId",
                table: "Arguments",
                newName: "IX_Arguments_OfferId");

            migrationBuilder.AddColumn<int>(
                name: "ArgumentInterventionId",
                table: "Arguments",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArgumentMessages",
                table: "ArgumentMessages",
                column: "ArgumentMessageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Arguments",
                table: "Arguments",
                column: "ArgumentId");

            migrationBuilder.CreateTable(
                name: "ArgumentInterventions",
                columns: table => new
                {
                    ArgumentInterventionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArgumentInterventions", x => x.ArgumentInterventionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arguments_ArgumentInterventionId",
                table: "Arguments",
                column: "ArgumentInterventionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArgumentMessages_Arguments_ArgumentId",
                table: "ArgumentMessages",
                column: "ArgumentId",
                principalTable: "Arguments",
                principalColumn: "ArgumentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Arguments_ArgumentInterventions_ArgumentInterventionId",
                table: "Arguments",
                column: "ArgumentInterventionId",
                principalTable: "ArgumentInterventions",
                principalColumn: "ArgumentInterventionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Arguments_Offers_OfferId",
                table: "Arguments",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "OfferId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Arguments_Users_StudentUserId",
                table: "Arguments",
                column: "StudentUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArgumentMessages_Arguments_ArgumentId",
                table: "ArgumentMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_Arguments_ArgumentInterventions_ArgumentInterventionId",
                table: "Arguments");

            migrationBuilder.DropForeignKey(
                name: "FK_Arguments_Offers_OfferId",
                table: "Arguments");

            migrationBuilder.DropForeignKey(
                name: "FK_Arguments_Users_StudentUserId",
                table: "Arguments");

            migrationBuilder.DropTable(
                name: "ArgumentInterventions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Arguments",
                table: "Arguments");

            migrationBuilder.DropIndex(
                name: "IX_Arguments_ArgumentInterventionId",
                table: "Arguments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArgumentMessages",
                table: "ArgumentMessages");

            migrationBuilder.DropColumn(
                name: "ArgumentInterventionId",
                table: "Arguments");

            migrationBuilder.RenameTable(
                name: "Arguments",
                newName: "Argument");

            migrationBuilder.RenameTable(
                name: "ArgumentMessages",
                newName: "ArgumentMessage");

            migrationBuilder.RenameIndex(
                name: "IX_Arguments_StudentUserId",
                table: "Argument",
                newName: "IX_Argument_StudentUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Arguments_OfferId",
                table: "Argument",
                newName: "IX_Argument_OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_ArgumentMessages_ArgumentId",
                table: "ArgumentMessage",
                newName: "IX_ArgumentMessage_ArgumentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Argument",
                table: "Argument",
                column: "ArgumentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArgumentMessage",
                table: "ArgumentMessage",
                column: "ArgumentMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Argument_Offers_OfferId",
                table: "Argument",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "OfferId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Argument_Users_StudentUserId",
                table: "Argument",
                column: "StudentUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArgumentMessage_Argument_ArgumentId",
                table: "ArgumentMessage",
                column: "ArgumentId",
                principalTable: "Argument",
                principalColumn: "ArgumentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
