using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Data.Migrations
{
    public partial class AddedFirstLastNamePhoneNumberAndDrivingLicenseNumberToTenantsRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DealerRequests_AspNetUsers_UserId",
                table: "DealerRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalApproveRequests_AspNetUsers_UserId",
                table: "RentalApproveRequests");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "RentalApproveRequests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "DrivingLicenseNumber",
                table: "RentalApproveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "RentalApproveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "RentalApproveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RentalApproveRequestId",
                table: "DrivingLicensePhotos",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "DealerRequests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_DrivingLicensePhotos_RentalApproveRequestId",
                table: "DrivingLicensePhotos",
                column: "RentalApproveRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_DealerRequests_AspNetUsers_UserId",
                table: "DealerRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingLicensePhotos_RentalApproveRequests_RentalApproveRequestId",
                table: "DrivingLicensePhotos",
                column: "RentalApproveRequestId",
                principalTable: "RentalApproveRequests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalApproveRequests_AspNetUsers_UserId",
                table: "RentalApproveRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DealerRequests_AspNetUsers_UserId",
                table: "DealerRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DrivingLicensePhotos_RentalApproveRequests_RentalApproveRequestId",
                table: "DrivingLicensePhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalApproveRequests_AspNetUsers_UserId",
                table: "RentalApproveRequests");

            migrationBuilder.DropIndex(
                name: "IX_DrivingLicensePhotos_RentalApproveRequestId",
                table: "DrivingLicensePhotos");

            migrationBuilder.DropColumn(
                name: "DrivingLicenseNumber",
                table: "RentalApproveRequests");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "RentalApproveRequests");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "RentalApproveRequests");

            migrationBuilder.DropColumn(
                name: "RentalApproveRequestId",
                table: "DrivingLicensePhotos");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "RentalApproveRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "DealerRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DealerRequests_AspNetUsers_UserId",
                table: "DealerRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalApproveRequests_AspNetUsers_UserId",
                table: "RentalApproveRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
