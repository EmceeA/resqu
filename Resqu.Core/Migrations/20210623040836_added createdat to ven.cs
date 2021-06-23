using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class addedcreatedattoven : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NextOfKinEmail",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextOfKinEmail",
                table: "Vendors");
        }
    }
}
