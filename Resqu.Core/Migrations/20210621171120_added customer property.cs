using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class addedcustomerproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "ResquServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "ResquServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVendorArrived",
                table: "ResquServices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "ResquServices");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "ResquServices");

            migrationBuilder.DropColumn(
                name: "IsVendorArrived",
                table: "ResquServices");
        }
    }
}
