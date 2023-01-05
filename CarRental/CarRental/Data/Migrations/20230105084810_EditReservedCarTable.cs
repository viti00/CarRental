using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Migrations
{
    public partial class EditReservedCarTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservedCars_AspNetUsers_ApplicationUserId",
                table: "ReservedCars");

            migrationBuilder.DropColumn(
                name: "TenantIndentificator",
                table: "ReservedCars");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "ReservedCars",
                newName: "TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservedCars_ApplicationUserId",
                table: "ReservedCars",
                newName: "IX_ReservedCars_TenantId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ReservedCars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedCars_AspNetUsers_TenantId",
                table: "ReservedCars",
                column: "TenantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservedCars_AspNetUsers_TenantId",
                table: "ReservedCars");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ReservedCars");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "ReservedCars",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservedCars_TenantId",
                table: "ReservedCars",
                newName: "IX_ReservedCars_ApplicationUserId");

            migrationBuilder.AddColumn<string>(
                name: "TenantIndentificator",
                table: "ReservedCars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedCars_AspNetUsers_ApplicationUserId",
                table: "ReservedCars",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
