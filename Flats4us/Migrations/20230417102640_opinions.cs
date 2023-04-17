using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flats4us.Migrations
{
    /// <inheritdoc />
    public partial class opinions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonOpinion");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "PropertyImage");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "PropertyImage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "OwnerOpinion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Check1 = table.Column<bool>(type: "bit", nullable: false),
                    Check2 = table.Column<bool>(type: "bit", nullable: false),
                    Check3 = table.Column<bool>(type: "bit", nullable: false),
                    Check4 = table.Column<bool>(type: "bit", nullable: false),
                    Check5 = table.Column<bool>(type: "bit", nullable: false),
                    Check6 = table.Column<bool>(type: "bit", nullable: false),
                    Check7 = table.Column<bool>(type: "bit", nullable: false),
                    EvaluatedId = table.Column<int>(type: "int", nullable: false),
                    EvaluatorId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerOpinion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnerOpinion_User_EvaluatedId",
                        column: x => x.EvaluatedId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OwnerOpinion_User_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OwnerOpinion_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OwnerOpinion_User_StudentId",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentOpinion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Check1 = table.Column<bool>(type: "bit", nullable: false),
                    Check2 = table.Column<bool>(type: "bit", nullable: false),
                    Check3 = table.Column<bool>(type: "bit", nullable: false),
                    Check4 = table.Column<bool>(type: "bit", nullable: false),
                    Check5 = table.Column<bool>(type: "bit", nullable: false),
                    Check6 = table.Column<bool>(type: "bit", nullable: false),
                    Check7 = table.Column<bool>(type: "bit", nullable: false),
                    EvaluatedId = table.Column<int>(type: "int", nullable: false),
                    EvaluatorId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentOpinion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentOpinion_User_EvaluatedId",
                        column: x => x.EvaluatedId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentOpinion_User_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentOpinion_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentOpinion_User_StudentId",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OwnerOpinion_EvaluatedId",
                table: "OwnerOpinion",
                column: "EvaluatedId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerOpinion_EvaluatorId",
                table: "OwnerOpinion",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerOpinion_OwnerId",
                table: "OwnerOpinion",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerOpinion_StudentId",
                table: "OwnerOpinion",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentOpinion_EvaluatedId",
                table: "StudentOpinion",
                column: "EvaluatedId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentOpinion_EvaluatorId",
                table: "StudentOpinion",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentOpinion_OwnerId",
                table: "StudentOpinion",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentOpinion_StudentId",
                table: "StudentOpinion",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnerOpinion");

            migrationBuilder.DropTable(
                name: "StudentOpinion");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "PropertyImage");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "User",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "PropertyImage",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateTable(
                name: "PersonOpinion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Check1 = table.Column<bool>(type: "bit", nullable: false),
                    Check2 = table.Column<bool>(type: "bit", nullable: false),
                    Check3 = table.Column<bool>(type: "bit", nullable: false),
                    Check4 = table.Column<bool>(type: "bit", nullable: false),
                    Check5 = table.Column<bool>(type: "bit", nullable: false),
                    Check6 = table.Column<bool>(type: "bit", nullable: false),
                    Check7 = table.Column<bool>(type: "bit", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonOpinion", x => x.Id);
                });
        }
    }
}
