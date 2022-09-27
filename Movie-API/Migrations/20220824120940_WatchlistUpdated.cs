using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie_API.Migrations
{
    public partial class WatchlistUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullTitle",
                table: "Watchlists",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IMDbRating",
                table: "Watchlists",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IMDbRatingCount",
                table: "Watchlists",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Watchlists",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullTitle",
                table: "Watchlists");

            migrationBuilder.DropColumn(
                name: "IMDbRating",
                table: "Watchlists");

            migrationBuilder.DropColumn(
                name: "IMDbRatingCount",
                table: "Watchlists");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Watchlists");
        }
    }
}
