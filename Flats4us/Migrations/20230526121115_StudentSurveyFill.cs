using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flats4us.Migrations
{
    /// <inheritdoc />
    public partial class StudentSurveyFill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SurveyStudents",
                keyColumn: "SurveyStudentId",
                keyValue: 5);

            migrationBuilder.RenameColumn(
                name: "test",
                table: "SurveyStudents",
                newName: "Tidiness");

            migrationBuilder.AddColumn<bool>(
                name: "Animals",
                table: "SurveyStudents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LookingForRoommate",
                table: "SurveyStudents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxNumberOfRoommates",
                table: "SurveyStudents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxRoommateAge",
                table: "SurveyStudents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinRoommateAge",
                table: "SurveyStudents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Party",
                table: "SurveyStudents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoommateGender",
                table: "SurveyStudents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Smoking",
                table: "SurveyStudents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Sociability",
                table: "SurveyStudents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Vegan",
                table: "SurveyStudents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Animals",
                table: "SurveyStudents");

            migrationBuilder.DropColumn(
                name: "LookingForRoommate",
                table: "SurveyStudents");

            migrationBuilder.DropColumn(
                name: "MaxNumberOfRoommates",
                table: "SurveyStudents");

            migrationBuilder.DropColumn(
                name: "MaxRoommateAge",
                table: "SurveyStudents");

            migrationBuilder.DropColumn(
                name: "MinRoommateAge",
                table: "SurveyStudents");

            migrationBuilder.DropColumn(
                name: "Party",
                table: "SurveyStudents");

            migrationBuilder.DropColumn(
                name: "RoommateGender",
                table: "SurveyStudents");

            migrationBuilder.DropColumn(
                name: "Smoking",
                table: "SurveyStudents");

            migrationBuilder.DropColumn(
                name: "Sociability",
                table: "SurveyStudents");

            migrationBuilder.DropColumn(
                name: "Vegan",
                table: "SurveyStudents");

            migrationBuilder.RenameColumn(
                name: "Tidiness",
                table: "SurveyStudents",
                newName: "test");

            migrationBuilder.InsertData(
                table: "SurveyStudents",
                columns: new[] { "SurveyStudentId", "test" },
                values: new object[] { 5, 1 });
        }
    }
}
