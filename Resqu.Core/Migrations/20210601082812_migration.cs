using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PageNameClass",
                table: "BackOfficeRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PageUrlClass",
                table: "BackOfficeRoles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageNameClass",
                table: "BackOfficeRoles");

            migrationBuilder.DropColumn(
                name: "PageUrlClass",
                table: "BackOfficeRoles");
        }
    }
}
