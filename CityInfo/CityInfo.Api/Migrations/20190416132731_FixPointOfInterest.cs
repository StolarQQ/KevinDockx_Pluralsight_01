using Microsoft.EntityFrameworkCore.Migrations;

namespace CityInfo.Api.Migrations
{
    public partial class FixPointOfInterest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                table: "PointsOfInterest");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "PointsOfInterest",
                nullable: false,
                defaultValue: 0);
        }
    }
}
