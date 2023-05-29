using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flats4us.Migrations
{
    /// <inheritdoc />
    public partial class OwnersurveysAdsPaymentsMeetingsOffers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpinionStudentOwner_Users_EvaluatedId",
                table: "OpinionStudentOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_OpinionStudentOwner_Users_EvaluatorId",
                table: "OpinionStudentOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyStudents_Users_SurveyStudentId",
                table: "SurveyStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyStudents",
                table: "SurveyStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpinionStudentOwner",
                table: "OpinionStudentOwner");

            migrationBuilder.RenameTable(
                name: "SurveyStudents",
                newName: "StudentSurveys");

            migrationBuilder.RenameTable(
                name: "OpinionStudentOwner",
                newName: "StudentOwnerOpinions");

            migrationBuilder.RenameIndex(
                name: "IX_OpinionStudentOwner_EvaluatorId",
                table: "StudentOwnerOpinions",
                newName: "IX_StudentOwnerOpinions_EvaluatorId");

            migrationBuilder.RenameIndex(
                name: "IX_OpinionStudentOwner_EvaluatedId",
                table: "StudentOwnerOpinions",
                newName: "IX_StudentOwnerOpinions_EvaluatedId");

            migrationBuilder.AddColumn<int>(
                name: "RoommatesStatus",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSurveys",
                table: "StudentSurveys",
                column: "SurveyStudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentOwnerOpinions",
                table: "StudentOwnerOpinions",
                column: "OpinionOwnerStudentId");

            migrationBuilder.CreateTable(
                name: "Advertisements",
                columns: table => new
                {
                    AdvertisementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BannerPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ModeratorUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisements", x => x.AdvertisementId);
                    table.ForeignKey(
                        name: "FK_Advertisements_Users_ModeratorUserId",
                        column: x => x.ModeratorUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    OfferId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfferStatus = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Decription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RentalPeriod = table.Column<int>(type: "int", nullable: false),
                    NumberOfInterested = table.Column<int>(type: "int", nullable: false),
                    Regulations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.OfferId);
                    table.ForeignKey(
                        name: "FK_Offers_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    MeetingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.MeetingId);
                    table.ForeignKey(
                        name: "FK_Meetings_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "OfferId");
                });

            migrationBuilder.CreateTable(
                name: "OfferInterests",
                columns: table => new
                {
                    OfferInterestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeekerUserId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferInterests", x => x.OfferInterestId);
                    table.ForeignKey(
                        name: "FK_OfferInterests_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferInterests_Users_SeekerUserId",
                        column: x => x.SeekerUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OwnerOfferSurveys",
                columns: table => new
                {
                    SurveyOwnerOfferId = table.Column<int>(type: "int", nullable: false),
                    NumberOfInhabitants = table.Column<int>(type: "int", nullable: false),
                    Smoking = table.Column<bool>(type: "bit", nullable: false),
                    Parties = table.Column<bool>(type: "bit", nullable: false),
                    Animals = table.Column<bool>(type: "bit", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    OwnerUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerOfferSurveys", x => x.SurveyOwnerOfferId);
                    table.ForeignKey(
                        name: "FK_OwnerOfferSurveys_Offers_SurveyOwnerOfferId",
                        column: x => x.SurveyOwnerOfferId,
                        principalTable: "Offers",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnerOfferSurveys_Users_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentPurpose = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    StudentUserId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Users_StudentUserId",
                        column: x => x.StudentUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetingStudent",
                columns: table => new
                {
                    MeetingsMeetingId = table.Column<int>(type: "int", nullable: false),
                    StudentsUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingStudent", x => new { x.MeetingsMeetingId, x.StudentsUserId });
                    table.ForeignKey(
                        name: "FK_MeetingStudent_Meetings_MeetingsMeetingId",
                        column: x => x.MeetingsMeetingId,
                        principalTable: "Meetings",
                        principalColumn: "MeetingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetingStudent_Users_StudentsUserId",
                        column: x => x.StudentsUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_ModeratorUserId",
                table: "Advertisements",
                column: "ModeratorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_OfferId",
                table: "Meetings",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingStudent_StudentsUserId",
                table: "MeetingStudent",
                column: "StudentsUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferInterests_OfferId",
                table: "OfferInterests",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferInterests_SeekerUserId",
                table: "OfferInterests",
                column: "SeekerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_PropertyId",
                table: "Offers",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerOfferSurveys_OwnerUserId",
                table: "OwnerOfferSurveys",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OfferId",
                table: "Payments",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StudentUserId",
                table: "Payments",
                column: "StudentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentOwnerOpinions_Users_EvaluatedId",
                table: "StudentOwnerOpinions",
                column: "EvaluatedId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentOwnerOpinions_Users_EvaluatorId",
                table: "StudentOwnerOpinions",
                column: "EvaluatorId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSurveys_Users_SurveyStudentId",
                table: "StudentSurveys",
                column: "SurveyStudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentOwnerOpinions_Users_EvaluatedId",
                table: "StudentOwnerOpinions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentOwnerOpinions_Users_EvaluatorId",
                table: "StudentOwnerOpinions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSurveys_Users_SurveyStudentId",
                table: "StudentSurveys");

            migrationBuilder.DropTable(
                name: "Advertisements");

            migrationBuilder.DropTable(
                name: "MeetingStudent");

            migrationBuilder.DropTable(
                name: "OfferInterests");

            migrationBuilder.DropTable(
                name: "OwnerOfferSurveys");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSurveys",
                table: "StudentSurveys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentOwnerOpinions",
                table: "StudentOwnerOpinions");

            migrationBuilder.DropColumn(
                name: "RoommatesStatus",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Properties");

            migrationBuilder.RenameTable(
                name: "StudentSurveys",
                newName: "SurveyStudents");

            migrationBuilder.RenameTable(
                name: "StudentOwnerOpinions",
                newName: "OpinionStudentOwner");

            migrationBuilder.RenameIndex(
                name: "IX_StudentOwnerOpinions_EvaluatorId",
                table: "OpinionStudentOwner",
                newName: "IX_OpinionStudentOwner_EvaluatorId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentOwnerOpinions_EvaluatedId",
                table: "OpinionStudentOwner",
                newName: "IX_OpinionStudentOwner_EvaluatedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyStudents",
                table: "SurveyStudents",
                column: "SurveyStudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpinionStudentOwner",
                table: "OpinionStudentOwner",
                column: "OpinionOwnerStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpinionStudentOwner_Users_EvaluatedId",
                table: "OpinionStudentOwner",
                column: "EvaluatedId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpinionStudentOwner_Users_EvaluatorId",
                table: "OpinionStudentOwner",
                column: "EvaluatorId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyStudents_Users_SurveyStudentId",
                table: "SurveyStudents",
                column: "SurveyStudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
