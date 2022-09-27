using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie_API.Migrations
{
    public partial class ReviewUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullTitle",
                table: "Reviews",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IMDbRating",
                table: "Reviews",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IMDbRatingCount",
                table: "Reviews",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Reviews",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullTitle",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "IMDbRating",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "IMDbRatingCount",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Reviews");
        }
    }
}
