using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GFapi.Migrations
{
    /// <inheritdoc />
    public partial class VideoURLandHairColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HairColor",
                table: "Actors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Actors",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HairColor",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Actors");
        }
    }
}
