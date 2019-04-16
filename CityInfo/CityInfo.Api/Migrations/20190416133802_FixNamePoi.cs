using Microsoft.EntityFrameworkCore.Migrations;

namespace CityInfo.Api.Migrations
{
    public partial class FixNamePoi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointsOfInterest_Cities_{CityId",
                table: "PointsOfInterest");

            migrationBuilder.RenameColumn(
                name: "{CityId",
                table: "PointsOfInterest",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_PointsOfInterest_{CityId",
                table: "PointsOfInterest",
                newName: "IX_PointsOfInterest_CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PointsOfInterest_Cities_CityId",
                table: "PointsOfInterest",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointsOfInterest_Cities_CityId",
                table: "PointsOfInterest");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "PointsOfInterest",
                newName: "{CityId");

            migrationBuilder.RenameIndex(
                name: "IX_PointsOfInterest_CityId",
                table: "PointsOfInterest",
                newName: "IX_PointsOfInterest_{CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PointsOfInterest_Cities_{CityId",
                table: "PointsOfInterest",
                column: "{CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
