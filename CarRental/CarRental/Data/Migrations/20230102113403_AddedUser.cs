using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Migrations
{
    public partial class AddedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ReservedCars",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenantIndentificator",
                table: "ReservedCars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Cars",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "CanRent",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DrivingLicensePhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Bytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrivingLicensePhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrivingLicensePhotos_AspNetUsers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservedCars_ApplicationUserId",
                table: "ReservedCars",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CreatorId",
                table: "Cars",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_DrivingLicensePhotos_DriverId",
                table: "DrivingLicensePhotos",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_CreatorId",
                table: "Cars",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedCars_AspNetUsers_ApplicationUserId",
                table: "ReservedCars",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_CreatorId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservedCars_AspNetUsers_ApplicationUserId",
                table: "ReservedCars");

            migrationBuilder.DropTable(
                name: "DrivingLicensePhotos");

            migrationBuilder.DropIndex(
                name: "IX_ReservedCars_ApplicationUserId",
                table: "ReservedCars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CreatorId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ReservedCars");

            migrationBuilder.DropColumn(
                name: "TenantIndentificator",
                table: "ReservedCars");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CanRent",
                table: "AspNetUsers");
        }
    }
}
