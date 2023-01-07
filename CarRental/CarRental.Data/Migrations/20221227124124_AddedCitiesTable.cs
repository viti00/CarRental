using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Data.Migrations
{
    public partial class AddedCitiesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CityId",
                table: "Cars",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Cities_CityId",
                table: "Cars",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Cities_CityId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CityId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Cars",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
