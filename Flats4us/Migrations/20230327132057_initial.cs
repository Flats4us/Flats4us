using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flats4us.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonOpinion",
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
                    Check7 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonOpinion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surface = table.Column<int>(type: "int", nullable: false),
                    MaxInhabitants = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfRooms = table.Column<int>(type: "int", nullable: true),
                    NumberOfFloors = table.Column<int>(type: "int", nullable: true),
                    ParcelSurface = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RentalPeriod = table.Column<int>(type: "int", nullable: false),
                    NumberOfIntrested = table.Column<int>(type: "int", nullable: false),
                    Regulations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offer_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyEquipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyEquipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyEquipment_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyEquipment_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyImage_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Advertisement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModeratorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferInterest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    SeekerId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferInterest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferInterest_Offer_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LengthInMonths = table.Column<int>(type: "int", nullable: false),
                    ContractInformations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rent_Offer_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentOpinion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Localization = table.Column<int>(type: "int", nullable: false),
                    Neighbors = table.Column<int>(type: "int", nullable: false),
                    Equipment = table.Column<int>(type: "int", nullable: false),
                    ParkingPlace = table.Column<int>(type: "int", nullable: false),
                    Tidiness = table.Column<int>(type: "int", nullable: false),
                    Decoration = table.Column<int>(type: "int", nullable: false),
                    Loudness = table.Column<int>(type: "int", nullable: false),
                    ComplianceWithOffer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentOpinion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentOpinion_Rent_Id",
                        column: x => x.Id,
                        principalTable: "Rent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Department = table.Column<int>(type: "int", nullable: true),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ActivityStatus = table.Column<bool>(type: "bit", nullable: true),
                    DocumentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentType = table.Column<int>(type: "int", nullable: true),
                    VerificationStatus = table.Column<int>(type: "int", nullable: true),
                    DocumentExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BankAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleDeedPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearOfBirth = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    StudentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RentId = table.Column<int>(type: "int", nullable: true),
                    RoommatesStatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Rent_RentId",
                        column: x => x.RentId,
                        principalTable: "Rent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Survey",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Smoking = table.Column<bool>(type: "bit", nullable: false),
                    Animal = table.Column<bool>(type: "bit", nullable: false),
                    Vegan = table.Column<bool>(type: "bit", nullable: false),
                    Party = table.Column<int>(type: "int", nullable: false),
                    Tidiness = table.Column<int>(type: "int", nullable: false),
                    Loudness = table.Column<int>(type: "int", nullable: false),
                    Sociability = table.Column<int>(type: "int", nullable: false),
                    Helpfulness = table.Column<int>(type: "int", nullable: false),
                    Roommate = table.Column<bool>(type: "bit", nullable: false),
                    MaxNumberOfRoommates = table.Column<int>(type: "int", nullable: false),
                    RoommateGender = table.Column<int>(type: "int", nullable: false),
                    MinRoommateAge = table.Column<int>(type: "int", nullable: false),
                    MaxRoommateAge = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Survey_User_Id",
                        column: x => x.Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_ModeratorId",
                table: "Advertisement",
                column: "ModeratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_PropertyId",
                table: "Offer",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferInterest_OfferId",
                table: "OfferInterest",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferInterest_SeekerId",
                table: "OfferInterest",
                column: "SeekerId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyEquipment_EquipmentId",
                table: "PropertyEquipment",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyEquipment_PropertyId",
                table: "PropertyEquipment",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImage_PropertyId",
                table: "PropertyImage",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Rent_OfferId",
                table: "Rent",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Rent_TenantId",
                table: "Rent",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RentId",
                table: "User",
                column: "RentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisement_User_ModeratorId",
                table: "Advertisement",
                column: "ModeratorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferInterest_User_SeekerId",
                table: "OfferInterest",
                column: "SeekerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_User_TenantId",
                table: "Rent",
                column: "TenantId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rent_User_TenantId",
                table: "Rent");

            migrationBuilder.DropTable(
                name: "Advertisement");

            migrationBuilder.DropTable(
                name: "OfferInterest");

            migrationBuilder.DropTable(
                name: "PersonOpinion");

            migrationBuilder.DropTable(
                name: "PropertyEquipment");

            migrationBuilder.DropTable(
                name: "PropertyImage");

            migrationBuilder.DropTable(
                name: "RentOpinion");

            migrationBuilder.DropTable(
                name: "Survey");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Rent");

            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropTable(
                name: "Property");
        }
    }
}
