using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Migrations
{
    public partial class SplitFirstAndLastNameFieldsForRentalApproveRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "RentalApproveRequests",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "RentalApproveRequests",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "RentalApproveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "RentalApproveRequests");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "RentalApproveRequests",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "RentalApproveRequests",
                newName: "FullName");
        }
    }
}
