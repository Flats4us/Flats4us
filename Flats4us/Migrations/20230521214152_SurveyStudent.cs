using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flats4us.Migrations
{
    /// <inheritdoc />
    public partial class SurveyStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ActivityStatus",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DocumentExpireDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentPath",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentType",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "University",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VerificationStatus",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearOfBirth",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SurveyStudents",
                columns: table => new
                {
                    SurveyStudentId = table.Column<int>(type: "int", nullable: false),
                    test = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyStudents", x => x.SurveyStudentId);
                    table.ForeignKey(
                        name: "FK_SurveyStudents_Users_SurveyStudentId",
                        column: x => x.SurveyStudentId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "AccountCreationDate", "HireDate", "LastLoginDate" },
                values: new object[] { new DateTime(2023, 5, 11, 23, 41, 52, 417, DateTimeKind.Local).AddTicks(2512), new DateTime(2023, 5, 11, 23, 41, 52, 417, DateTimeKind.Local).AddTicks(2555), new DateTime(2023, 5, 21, 23, 41, 52, 417, DateTimeKind.Local).AddTicks(2553) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "AccountCreationDate", "HireDate", "LastLoginDate" },
                values: new object[] { new DateTime(2023, 5, 16, 23, 41, 52, 417, DateTimeKind.Local).AddTicks(2557), new DateTime(2023, 5, 16, 23, 41, 52, 417, DateTimeKind.Local).AddTicks(2560), new DateTime(2023, 5, 20, 23, 41, 52, 417, DateTimeKind.Local).AddTicks(2559) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "AccountCreationDate", "HireDate", "LastLoginDate" },
                values: new object[] { new DateTime(2023, 5, 14, 23, 41, 52, 417, DateTimeKind.Local).AddTicks(2563), new DateTime(2023, 5, 14, 23, 41, 52, 417, DateTimeKind.Local).AddTicks(2566), new DateTime(2023, 5, 19, 23, 41, 52, 417, DateTimeKind.Local).AddTicks(2564) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "AccountCreationDate", "HireDate", "LastLoginDate" },
                values: new object[] { new DateTime(2023, 5, 18, 23, 41, 52, 417, DateTimeKind.Local).AddTicks(2567), new DateTime(2023, 5, 18, 23, 41, 52, 417, DateTimeKind.Local).AddTicks(2570), new DateTime(2023, 5, 20, 23, 41, 52, 417, DateTimeKind.Local).AddTicks(2569) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AccountCreationDate", "ActivityStatus", "Age", "City", "Discriminator", "DocumentExpireDate", "DocumentPath", "DocumentType", "Email", "Facebook", "Flat", "Instagram", "LastLoginDate", "Name", "Number", "Password", "PhoneNumber", "PhotoPath", "PostalCode", "Street", "StudentNumber", "Surname", "Twitter", "University", "VerificationStatus", "YearOfBirth" },
                values: new object[] { 5, new DateTime(2023, 5, 11, 23, 41, 52, 418, DateTimeKind.Local).AddTicks(7316), false, 22, "Warszawa", "Seeker", new DateTime(2024, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "/documents/1", 0, "maciej.kowalski@gmail.com", "", 2, "", new DateTime(2023, 5, 21, 23, 41, 52, 418, DateTimeKind.Local).AddTicks(7331), "Maciej", 47, "mkowalski123", "456736829", "/profile/1", "00-000", "Długa", "s22523", "Kowalski", "", "PJATK", 0, 2001 });

            migrationBuilder.InsertData(
                table: "SurveyStudents",
                columns: new[] { "SurveyStudentId", "test" },
                values: new object[] { 5, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyStudents");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "ActivityStatus",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DocumentExpireDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DocumentPath",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StudentNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "University",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VerificationStatus",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "YearOfBirth",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "AccountCreationDate", "HireDate", "LastLoginDate" },
                values: new object[] { new DateTime(2023, 5, 11, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6863), new DateTime(2023, 5, 11, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6913), new DateTime(2023, 5, 21, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6911) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "AccountCreationDate", "HireDate", "LastLoginDate" },
                values: new object[] { new DateTime(2023, 5, 16, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6916), new DateTime(2023, 5, 16, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6920), new DateTime(2023, 5, 20, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6918) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "AccountCreationDate", "HireDate", "LastLoginDate" },
                values: new object[] { new DateTime(2023, 5, 14, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6922), new DateTime(2023, 5, 14, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6925), new DateTime(2023, 5, 19, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6924) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "AccountCreationDate", "HireDate", "LastLoginDate" },
                values: new object[] { new DateTime(2023, 5, 18, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6928), new DateTime(2023, 5, 18, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6931), new DateTime(2023, 5, 20, 21, 47, 17, 869, DateTimeKind.Local).AddTicks(6929) });
        }
    }
}
