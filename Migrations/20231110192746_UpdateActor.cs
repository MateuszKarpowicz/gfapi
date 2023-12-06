using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GFapi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateActor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Actors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "Actors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EyeColor",
                table: "Actors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "Actors",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                table: "Actors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Actors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Skills",
                table: "Actors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Actors",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "EyeColor",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "Languages",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "Skills",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Actors");
        }
    }
}
