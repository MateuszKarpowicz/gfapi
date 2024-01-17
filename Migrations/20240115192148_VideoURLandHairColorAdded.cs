using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GFapi.Migrations
{
    /// <inheritdoc />
    public partial class VideoURLandHairColorAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Height",
                table: "Actors",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Height",
                table: "Actors",
                type: "real",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
