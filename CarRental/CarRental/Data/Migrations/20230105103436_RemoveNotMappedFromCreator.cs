using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Migrations
{
    public partial class RemoveNotMappedFromCreator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_ApplicationUserId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservedCars_Cars_CarId",
                table: "ReservedCars");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Cars",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_ApplicationUserId",
                table: "Cars",
                newName: "IX_Cars_CreatorId");

            migrationBuilder.AlterColumn<string>(
                name: "CarId",
                table: "ReservedCars",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_CreatorId",
                table: "Cars",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedCars_Cars_CarId",
                table: "ReservedCars",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_CreatorId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservedCars_Cars_CarId",
                table: "ReservedCars");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Cars",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_CreatorId",
                table: "Cars",
                newName: "IX_Cars_ApplicationUserId");

            migrationBuilder.AlterColumn<string>(
                name: "CarId",
                table: "ReservedCars",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_ApplicationUserId",
                table: "Cars",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedCars_Cars_CarId",
                table: "ReservedCars",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
