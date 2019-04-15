using Microsoft.EntityFrameworkCore.Migrations;

namespace CityInfo.Api.Migrations
{
    public partial class FixPointSyntax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointOfInterests_Cities_{CityId",
                table: "PointOfInterests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PointOfInterests",
                table: "PointOfInterests");

            migrationBuilder.RenameTable(
                name: "PointOfInterests",
                newName: "PointsOfInterest");

            migrationBuilder.RenameIndex(
                name: "IX_PointOfInterests_{CityId",
                table: "PointsOfInterest",
                newName: "IX_PointsOfInterest_{CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PointsOfInterest",
                table: "PointsOfInterest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PointsOfInterest_Cities_{CityId",
                table: "PointsOfInterest",
                column: "{CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointsOfInterest_Cities_{CityId",
                table: "PointsOfInterest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PointsOfInterest",
                table: "PointsOfInterest");

            migrationBuilder.RenameTable(
                name: "PointsOfInterest",
                newName: "PointOfInterests");

            migrationBuilder.RenameIndex(
                name: "IX_PointsOfInterest_{CityId",
                table: "PointOfInterests",
                newName: "IX_PointOfInterests_{CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PointOfInterests",
                table: "PointOfInterests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PointOfInterests_Cities_{CityId",
                table: "PointOfInterests",
                column: "{CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
