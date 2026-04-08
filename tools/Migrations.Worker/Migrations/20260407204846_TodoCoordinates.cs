using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations.Worker.Migrations
{
    /// <inheritdoc />
    public partial class TodoCoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Coordinate_Latitude",
                table: "Todo",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Coordinate_Longitude",
                table: "Todo",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Todo",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinate_Latitude",
                table: "Todo");

            migrationBuilder.DropColumn(
                name: "Coordinate_Longitude",
                table: "Todo");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Todo");
        }
    }
}
