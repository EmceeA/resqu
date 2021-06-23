using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class addedbookingidtovendorrating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookingId",
                table: "VendorRatings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "VendorRatings");
        }
    }
}
