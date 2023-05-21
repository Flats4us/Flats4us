using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flats4us.Migrations
{
    /// <inheritdoc />
    public partial class Interests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interests",
                columns: table => new
                {
                    InterestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.InterestId);
                });

            migrationBuilder.CreateTable(
                name: "InterestsStudent",
                columns: table => new
                {
                    InterestsInterestId = table.Column<int>(type: "int", nullable: false),
                    StudentsUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestsStudent", x => new { x.InterestsInterestId, x.StudentsUserId });
                    table.ForeignKey(
                        name: "FK_InterestsStudent_Interests_InterestsInterestId",
                        column: x => x.InterestsInterestId,
                        principalTable: "Interests",
                        principalColumn: "InterestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterestsStudent_Users_StudentsUserId",
                        column: x => x.StudentsUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "AccountCreationDate", "HireDate", "LastLoginDate" },
                values: new object[] { new DateTime(2023, 5, 11, 23, 54, 31, 979, DateTimeKind.Local).AddTicks(3581), new DateTime(2023, 5, 11, 23, 54, 31, 979, DateTimeKind.Local).AddTicks(3622), new DateTime(2023, 5, 21, 23, 54, 31, 979, DateTimeKind.Local).AddTicks(3621) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "AccountCreationDate", "HireDate", "LastLoginDate" },
                values: new object[] { new DateTime(2023, 5, 16, 23, 54, 31, 979, DateTimeKind.Local).AddTicks(3627), new DateTime(2023, 5, 16, 23, 54, 31, 979, DateTimeKind.Local).AddTicks(3630), new DateTime(2023, 5, 20, 23, 54, 31, 979, DateTimeKind.Local).AddTicks(3628) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "AccountCreationDate", "HireDate", "LastLoginDate" },
                values: new object[] { new DateTime(2023, 5, 14, 23, 54, 31, 979, DateTimeKind.Local).AddTicks(3632), new DateTime(2023, 5, 14, 23, 54, 31, 979, DateTimeKind.Local).AddTicks(3636), new DateTime(2023, 5, 19, 23, 54, 31, 979, DateTimeKind.Local).AddTicks(3634) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "AccountCreationDate", "HireDate", "LastLoginDate" },
                values: new object[] { new DateTime(2023, 5, 18, 23, 54, 31, 979, DateTimeKind.Local).AddTicks(3637), new DateTime(2023, 5, 18, 23, 54, 31, 979, DateTimeKind.Local).AddTicks(3640), new DateTime(2023, 5, 20, 23, 54, 31, 979, DateTimeKind.Local).AddTicks(3639) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                columns: new[] { "AccountCreationDate", "LastLoginDate" },
                values: new object[] { new DateTime(2023, 5, 11, 23, 54, 31, 980, DateTimeKind.Local).AddTicks(8516), new DateTime(2023, 5, 21, 23, 54, 31, 980, DateTimeKind.Local).AddTicks(8540) });

            migrationBuilder.CreateIndex(
                name: "IX_InterestsStudent_StudentsUserId",
                table: "InterestsStudent",
                column: "StudentsUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterestsStudent");

            migrationBuilder.DropTable(
                name: "Interests");

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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                columns: new[] { "AccountCreationDate", "LastLoginDate" },
                values: new object[] { new DateTime(2023, 5, 11, 23, 41, 52, 418, DateTimeKind.Local).AddTicks(7316), new DateTime(2023, 5, 21, 23, 41, 52, 418, DateTimeKind.Local).AddTicks(7331) });
        }
    }
}
