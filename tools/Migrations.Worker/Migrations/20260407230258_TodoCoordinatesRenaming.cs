using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations.Worker.Migrations
{
    /// <inheritdoc />
    public partial class TodoCoordinatesRenaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Coordinate_Longitude",
                table: "Todo",
                newName: "Coordinates_Longitude");

            migrationBuilder.RenameColumn(
                name: "Coordinate_Latitude",
                table: "Todo",
                newName: "Coordinates_Latitude");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Coordinates_Longitude",
                table: "Todo",
                newName: "Coordinate_Longitude");

            migrationBuilder.RenameColumn(
                name: "Coordinates_Latitude",
                table: "Todo",
                newName: "Coordinate_Latitude");
        }
    }
}
