using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class addedservicetyoetovendorrating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceType",
                table: "VendorRatings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "VendorRatings");
        }
    }
}
