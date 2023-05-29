using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flats4us.Migrations
{
    /// <inheritdoc />
    public partial class ArgumentIntervention_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModeratorUserId",
                table: "ArgumentInterventions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ArgumentInterventions_ModeratorUserId",
                table: "ArgumentInterventions",
                column: "ModeratorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArgumentInterventions_Users_ModeratorUserId",
                table: "ArgumentInterventions",
                column: "ModeratorUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArgumentInterventions_Users_ModeratorUserId",
                table: "ArgumentInterventions");

            migrationBuilder.DropIndex(
                name: "IX_ArgumentInterventions_ModeratorUserId",
                table: "ArgumentInterventions");

            migrationBuilder.DropColumn(
                name: "ModeratorUserId",
                table: "ArgumentInterventions");
        }
    }
}
