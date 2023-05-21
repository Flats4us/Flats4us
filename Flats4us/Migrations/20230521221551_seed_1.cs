using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Flats4us.Migrations
{
    /// <inheritdoc />
    public partial class seed_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterestsStudent");

            migrationBuilder.CreateTable(
                name: "InterestStudent",
                columns: table => new
                {
                    InterestsInterestId = table.Column<int>(type: "int", nullable: false),
                    StudentsUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestStudent", x => new { x.InterestsInterestId, x.StudentsUserId });
                    table.ForeignKey(
                        name: "FK_InterestStudent_Interests_InterestsInterestId",
                        column: x => x.InterestsInterestId,
                        principalTable: "Interests",
                        principalColumn: "InterestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterestStudent_Users_StudentsUserId",
                        column: x => x.StudentsUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Interests",
                columns: new[] { "InterestId", "Name" },
                values: new object[,]
                {
                    { 1, "Sailing" },
                    { 2, "Painting" },
                    { 3, "Photography" },
                    { 4, "Cooking" },
                    { 5, "Gardening" },
                    { 6, "Playing Guitar" },
                    { 7, "Reading Books" },
                    { 8, "Hiking" },
                    { 9, "Dancing" },
                    { 10, "Yoga" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "AccountCreationDate", "HireDate", "LastLoginDate" },
                values: new object[] { new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "AccountCreationDate", "HireDate", "LastLoginDate" },
                values: new object[] { new DateTime(2023, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "AccountCreationDate", "HireDate", "LastLoginDate" },
                values: new object[] { new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "AccountCreationDate", "HireDate", "LastLoginDate" },
                values: new object[] { new DateTime(2023, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                columns: new[] { "AccountCreationDate", "DocumentType", "Email", "Flat", "LastLoginDate", "Name", "Number", "Password", "Street", "StudentNumber", "Surname" },
                values: new object[] { new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "filip.dobrej@gmail.com", 3, new DateTime(2023, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Filip", 7, "fdobrej123", "Krótka", "s22023", "Dobrej" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AccountCreationDate", "ActivityStatus", "Age", "City", "Discriminator", "DocumentExpireDate", "DocumentPath", "DocumentType", "Email", "Facebook", "Flat", "Instagram", "LastLoginDate", "Name", "Number", "Password", "PhoneNumber", "PhotoPath", "PostalCode", "Street", "StudentNumber", "Surname", "Twitter", "University", "VerificationStatus", "YearOfBirth" },
                values: new object[,]
                {
                    { 6, new DateTime(2022, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 28, "Poznań", "Seeker", new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "/documents/2", 2, "alicja.dabrowska@gmail.com", "https://www.facebook.com/alicja.dabrowska", 4, "https://www.instagram.com/alicja_dabrowska/", new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alicja", 9, "adabrowska789", "601234567", "/profile/2", "60-001", "Kościuszki", "s23423", "Dąbrowska", "", "Uniwersytet Warszawski", 0, 1995 },
                    { 7, new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 25, "Kraków", "Seeker", new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "/documents/3", 0, "dominik.kowalczyk@gmail.com", "https://www.facebook.com/dominik.kowalczyk", 1, "https://www.instagram.com/dominik_kowalczyk/", new DateTime(2023, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dominik", 12, "dkowalczyk456", "712345678", "/profile/3", "30-002", "Szkolna", "s22345", "Kowalczyk", "", "AGH University of Science and Technology", 0, 1998 },
                    { 8, new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 23, "Warszawa", "Seeker", new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "/documents/4", 2, "natalia.wojcik@gmail.com", "", 2, "https://www.instagram.com/natalia_wojcik/", new DateTime(2023, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Natalia", 5, "nwojcik789", "601234567", "/profile/4", "02-001", "Kwiatowa", "s22890", "Wójcik", "https://twitter.com/nwojcik", "University of Warsaw", 0, 2000 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterestStudent_StudentsUserId",
                table: "InterestStudent",
                column: "StudentsUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterestStudent");

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "InterestId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "InterestId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "InterestId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "InterestId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "InterestId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "InterestId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "InterestId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "InterestId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "InterestId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "InterestId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8);

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
                columns: new[] { "AccountCreationDate", "DocumentType", "Email", "Flat", "LastLoginDate", "Name", "Number", "Password", "Street", "StudentNumber", "Surname" },
                values: new object[] { new DateTime(2023, 5, 11, 23, 54, 31, 980, DateTimeKind.Local).AddTicks(8516), 0, "maciej.kowalski@gmail.com", 2, new DateTime(2023, 5, 21, 23, 54, 31, 980, DateTimeKind.Local).AddTicks(8540), "Maciej", 47, "mkowalski123", "Długa", "s22523", "Kowalski" });

            migrationBuilder.CreateIndex(
                name: "IX_InterestsStudent_StudentsUserId",
                table: "InterestsStudent",
                column: "StudentsUserId");
        }
    }
}
