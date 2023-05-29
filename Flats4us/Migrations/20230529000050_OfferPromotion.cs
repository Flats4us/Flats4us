using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flats4us.Migrations
{
    /// <inheritdoc />
    public partial class OfferPromotion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OfferPromotions",
                columns: table => new
                {
                    OfferPromotionId = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferPromotions");
        }
    }
}
