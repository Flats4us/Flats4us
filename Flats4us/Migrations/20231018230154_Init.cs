using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Flats4us.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    EquipmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.EquipmentId);
                });

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
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Flat = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivityStatus = table.Column<bool>(type: "bit", nullable: true),
                    DocumentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentType = table.Column<int>(type: "int", nullable: true),
                    VerificationStatus = table.Column<int>(type: "int", nullable: true),
                    DocumentExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BankAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearOfBirth = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    StudentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    University = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instagram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoommatesStatus = table.Column<int>(type: "int", nullable: true),
                    IsTenant = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

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
                name: "ArgumentInterventions",
                columns: table => new
                {
                    ArgumentInterventionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModeratorUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArgumentInterventions", x => x.ArgumentInterventionId);
                    table.ForeignKey(
                        name: "FK_ArgumentInterventions_Users_ModeratorUserId",
                        column: x => x.ModeratorUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    ChatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.ChatId);
                    table.ForeignKey(
                        name: "FK_Chats_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chats_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "OwnerStudentOpinions",
                columns: table => new
                {
                    OpinionOwnerStudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Check1 = table.Column<int>(type: "int", nullable: false),
                    EvaluatedId = table.Column<int>(type: "int", nullable: false),
                    EvaluatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerStudentOpinions", x => x.OpinionOwnerStudentId);
                    table.ForeignKey(
                        name: "FK_OwnerStudentOpinions_Users_EvaluatedId",
                        column: x => x.EvaluatedId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OwnerStudentOpinions_Users_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Flat = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<int>(type: "int", nullable: false),
                    MaxNumberOfInhabitants = table.Column<int>(type: "int", nullable: false),
                    ConstructionYear = table.Column<int>(type: "int", nullable: false),
                    ImagesPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerificationStatus = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfRooms = table.Column<int>(type: "int", nullable: true),
                    Floor = table.Column<int>(type: "int", nullable: true),
                    Elevator = table.Column<bool>(type: "bit", nullable: true),
                    NumberOfFloors = table.Column<int>(type: "int", nullable: true),
                    PlotArea = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_Properties_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentOwnerOpinions",
                columns: table => new
                {
                    OpinionOwnerStudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Check1 = table.Column<int>(type: "int", nullable: false),
                    EvaluatedId = table.Column<int>(type: "int", nullable: false),
                    EvaluatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentOwnerOpinions", x => x.OpinionOwnerStudentId);
                    table.ForeignKey(
                        name: "FK_StudentOwnerOpinions_Users_EvaluatedId",
                        column: x => x.EvaluatedId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentOwnerOpinions_Users_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentStudentOpinions",
                columns: table => new
                {
                    OpinionStudentStudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Check1 = table.Column<int>(type: "int", nullable: false),
                    EvaluatedId = table.Column<int>(type: "int", nullable: false),
                    EvaluatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentStudentOpinions", x => x.OpinionStudentStudentId);
                    table.ForeignKey(
                        name: "FK_StudentStudentOpinions_Users_EvaluatedId",
                        column: x => x.EvaluatedId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentStudentOpinions_Users_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentSurveys",
                columns: table => new
                {
                    SurveyStudentId = table.Column<int>(type: "int", nullable: false),
                    Party = table.Column<int>(type: "int", nullable: false),
                    Tidiness = table.Column<int>(type: "int", nullable: false),
                    Smoking = table.Column<bool>(type: "bit", nullable: false),
                    Sociability = table.Column<int>(type: "int", nullable: false),
                    Animals = table.Column<bool>(type: "bit", nullable: false),
                    Vegan = table.Column<bool>(type: "bit", nullable: false),
                    LookingForRoommate = table.Column<bool>(type: "bit", nullable: false),
                    MaxNumberOfRoommates = table.Column<int>(type: "int", nullable: false),
                    RoommateGender = table.Column<int>(type: "int", nullable: false),
                    MinRoommateAge = table.Column<int>(type: "int", nullable: false),
                    MaxRoommateAge = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSurveys", x => x.SurveyStudentId);
                    table.ForeignKey(
                        name: "FK_StudentSurveys_Users_SurveyStudentId",
                        column: x => x.SurveyStudentId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    ChatMessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sender = table.Column<int>(type: "int", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.ChatMessageId);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentProperty",
                columns: table => new
                {
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    PropertiesPropertyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentProperty", x => new { x.EquipmentId, x.PropertiesPropertyId });
                    table.ForeignKey(
                        name: "FK_EquipmentProperty_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "EquipmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentProperty_Properties_PropertiesPropertyId",
                        column: x => x.PropertiesPropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
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
                name: "Arguments",
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
                    StudentUserId = table.Column<int>(type: "int", nullable: false),
                    ArgumentInterventionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arguments", x => x.ArgumentId);
                    table.ForeignKey(
                        name: "FK_Arguments_ArgumentInterventions_ArgumentInterventionId",
                        column: x => x.ArgumentInterventionId,
                        principalTable: "ArgumentInterventions",
                        principalColumn: "ArgumentInterventionId");
                    table.ForeignKey(
                        name: "FK_Arguments_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Arguments_Users_StudentUserId",
                        column: x => x.StudentUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
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
                    StudentUserId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_OfferInterests_Users_StudentUserId",
                        column: x => x.StudentUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferPromotions",
                columns: table => new
                {
                    OfferPromotionId = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    PricePerDay = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferPromotions", x => x.OfferPromotionId);
                    table.ForeignKey(
                        name: "FK_OfferPromotions_Offers_OfferPromotionId",
                        column: x => x.OfferPromotionId,
                        principalTable: "Offers",
                        principalColumn: "OfferId",
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
                name: "Rents",
                columns: table => new
                {
                    RentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RentPeriod = table.Column<int>(type: "int", nullable: false),
                    ContractInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OffersOfferId = table.Column<int>(type: "int", nullable: false),
                    StudentUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.RentId);
                    table.ForeignKey(
                        name: "FK_Rents_Offers_OffersOfferId",
                        column: x => x.OffersOfferId,
                        principalTable: "Offers",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rents_Users_StudentUserId",
                        column: x => x.StudentUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArgumentMessages",
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
                    table.PrimaryKey("PK_ArgumentMessages", x => x.ArgumentMessageId);
                    table.ForeignKey(
                        name: "FK_ArgumentMessages_Arguments_ArgumentId",
                        column: x => x.ArgumentId,
                        principalTable: "Arguments",
                        principalColumn: "ArgumentId",
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

            migrationBuilder.CreateTable(
                name: "RentOpinions",
                columns: table => new
                {
                    OpinionRentId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<int>(type: "int", nullable: false),
                    Neighbors = table.Column<int>(type: "int", nullable: false),
                    Equipment = table.Column<int>(type: "int", nullable: false),
                    ParkingSpace = table.Column<int>(type: "int", nullable: false),
                    Tidiness = table.Column<int>(type: "int", nullable: false),
                    Decoration = table.Column<int>(type: "int", nullable: false),
                    Noisiness = table.Column<int>(type: "int", nullable: false),
                    ComplianceWithOffer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentOpinions", x => x.OpinionRentId);
                    table.ForeignKey(
                        name: "FK_RentOpinions_Rents_OpinionRentId",
                        column: x => x.OpinionRentId,
                        principalTable: "Rents",
                        principalColumn: "RentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AccountCreationDate", "ActivityStatus", "BankAccount", "City", "Discriminator", "DocumentExpireDate", "DocumentPath", "DocumentType", "Email", "Flat", "LastLoginDate", "Name", "Number", "Password", "PhoneNumber", "PhotoPath", "PostalCode", "Street", "Surname", "VerificationStatus" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "12341234123412341234123412", "Warszawa", "Owner", new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "placeholder", 0, "mkowalski@gmail.com", 2, new DateTime(2023, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maciej", 54, "mkowalski123", "123456789", "placeholder", "00-000", "Marszałkowska", "Kowalski", 0 },
                    { 2, new DateTime(2023, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "12341234123412341234123412", "Kraków", "Owner", new DateTime(2025, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "placeholder", 0, "bnowak@gmail.com", null, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Barbara", 12, "bnowak123", "123456789", "placeholder", "00-000", "Długa", "Nowak", 0 },
                    { 3, new DateTime(2023, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "12341234123412341234123412", "Łódż", "Owner", new DateTime(2029, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "placeholder", 0, "rzielinski@gmail.com", 7, new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rafał", 99, "rzielinski123", "123456789", "placeholder", "00-000", "Prosta", "Zieliński", 0 }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyId", "Area", "City", "ConstructionYear", "Discriminator", "District", "Elevator", "Flat", "Floor", "ImagesPath", "MaxNumberOfInhabitants", "Number", "NumberOfRooms", "OwnerId", "PostalCode", "Province", "Street", "VerificationStatus" },
                values: new object[,]
                {
                    { 1, 40, "Warszawa", 2000, "Flat", "Wilanów", false, 3, 2, "e2c2baa7-3dd8-490f-8378-972cf167c004", 2, "4", 2, 1, "00-000", "Mazowieckie", "Radosna", 0 },
                    { 2, 50, "Warszawa", 1995, "Flat", "Ursynów", true, 1, 4, "1bb1e30b-2c30-47a2-8047-220649fa5df8", 2, "9", 3, 2, "00-000", "Mazowieckie", "Lasek Brzozowy", 0 },
                    { 3, 80, "Warszawa", 2010, "Flat", "Włochy", true, 5, 3, "988aae23-a539-43c5-a7a0-35ab823a40af", 3, "2", 4, 3, "00-000", "Mazowieckie", "Żegoty", 0 },
                    { 4, 55, "Warszawa", 2002, "Flat", "Bielany", true, 2, 5, "796fe042-d1bd-46c5-a5f3-e24efe1373a3", 2, "1", 3, 2, "00-000", "Mazowieckie", "Biograficzna", 0 },
                    { 5, 45, "Warszawa", 1998, "Flat", "Mokotów", false, 1, 3, "6bdfc44b-a0ef-4fd6-9ac4-09fa6f98e447", 2, "18", 2, 1, "00-000", "Mazowieckie", "Samochodowa", 0 }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyId", "Area", "City", "ConstructionYear", "Discriminator", "District", "Elevator", "Flat", "Floor", "ImagesPath", "MaxNumberOfInhabitants", "Number", "OwnerId", "PostalCode", "Province", "Street", "VerificationStatus" },
                values: new object[,]
                {
                    { 6, 40, "Warszawa", 2000, "Room", "Wilanów", false, 3, 2, "107d5997-dc99-48af-bd69-cc698a8649e6", 2, "5b", 1, "00-000", "Małopolskie", "Świętej Urszuli Ledóchowskiej", 0 },
                    { 7, 50, "Warszawa", 1995, "Room", "Targówek", true, null, 1, "fdf28490-2de7-4b7c-85c1-c5bfe1003e24", 2, "8", 2, "00-000", "Mazowieckie", "Muszyniecka", 0 },
                    { 8, 80, "Warszawa", 2010, "Room", "Wawer", true, 1, 3, "8fe4e3d1-4096-4556-aba0-98969eac7203", 3, "25", 3, "00-000", "Mazowieckie", "Gruntowa", 0 },
                    { 9, 55, "Warszawa", 2002, "Room", "Mokotów", true, 2, 5, "9ada2eaf-9b59-48d4-b9fd-6f70f27df0fe", 2, "30", 2, "00-000", "Mazowieckie", "Goworka", 0 },
                    { 10, 45, "Piaseczno", 1998, "Room", "Mokotów", false, 7, 3, "5cf06a97-6830-46de-8bbd-d2c3771357c6", 2, "3", 1, "00-000", "Mazowieckie", "Ludwiga van Beethovena", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_ModeratorUserId",
                table: "Advertisements",
                column: "ModeratorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArgumentInterventions_ModeratorUserId",
                table: "ArgumentInterventions",
                column: "ModeratorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArgumentMessages_ArgumentId",
                table: "ArgumentMessages",
                column: "ArgumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Arguments_ArgumentInterventionId",
                table: "Arguments",
                column: "ArgumentInterventionId");

            migrationBuilder.CreateIndex(
                name: "IX_Arguments_OfferId",
                table: "Arguments",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Arguments_StudentUserId",
                table: "Arguments",
                column: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatId",
                table: "ChatMessages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_OwnerId",
                table: "Chats",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_StudentId",
                table: "Chats",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentProperty_PropertiesPropertyId",
                table: "EquipmentProperty",
                column: "PropertiesPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_InterestStudent_StudentsUserId",
                table: "InterestStudent",
                column: "StudentsUserId");

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
                name: "IX_OfferInterests_StudentUserId",
                table: "OfferInterests",
                column: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_PropertyId",
                table: "Offers",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerOfferSurveys_OwnerUserId",
                table: "OwnerOfferSurveys",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerStudentOpinions_EvaluatedId",
                table: "OwnerStudentOpinions",
                column: "EvaluatedId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerStudentOpinions_EvaluatorId",
                table: "OwnerStudentOpinions",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OfferId",
                table: "Payments",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StudentUserId",
                table: "Payments",
                column: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OwnerId",
                table: "Properties",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_OffersOfferId",
                table: "Rents",
                column: "OffersOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_StudentUserId",
                table: "Rents",
                column: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentOwnerOpinions_EvaluatedId",
                table: "StudentOwnerOpinions",
                column: "EvaluatedId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentOwnerOpinions_EvaluatorId",
                table: "StudentOwnerOpinions",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentStudentOpinions_EvaluatedId",
                table: "StudentStudentOpinions",
                column: "EvaluatedId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentStudentOpinions_EvaluatorId",
                table: "StudentStudentOpinions",
                column: "EvaluatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisements");

            migrationBuilder.DropTable(
                name: "ArgumentMessages");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "EquipmentProperty");

            migrationBuilder.DropTable(
                name: "InterestStudent");

            migrationBuilder.DropTable(
                name: "MeetingStudent");

            migrationBuilder.DropTable(
                name: "OfferInterests");

            migrationBuilder.DropTable(
                name: "OfferPromotions");

            migrationBuilder.DropTable(
                name: "OwnerOfferSurveys");

            migrationBuilder.DropTable(
                name: "OwnerStudentOpinions");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "RentOpinions");

            migrationBuilder.DropTable(
                name: "StudentOwnerOpinions");

            migrationBuilder.DropTable(
                name: "StudentStudentOpinions");

            migrationBuilder.DropTable(
                name: "StudentSurveys");

            migrationBuilder.DropTable(
                name: "Arguments");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Interests");

            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "Rents");

            migrationBuilder.DropTable(
                name: "ArgumentInterventions");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
