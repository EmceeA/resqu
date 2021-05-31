using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class addeduser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BackOfficeRoleId",
                table: "BackOfficeUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PageName",
                table: "BackOfficeRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PageUrl",
                table: "BackOfficeRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BackOfficeUsers_BackOfficeRoleId",
                table: "BackOfficeUsers",
                column: "BackOfficeRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_BackOfficeUsers_BackOfficeRoles_BackOfficeRoleId",
                table: "BackOfficeUsers",
                column: "BackOfficeRoleId",
                principalTable: "BackOfficeRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BackOfficeUsers_BackOfficeRoles_BackOfficeRoleId",
                table: "BackOfficeUsers");

            migrationBuilder.DropIndex(
                name: "IX_BackOfficeUsers_BackOfficeRoleId",
                table: "BackOfficeUsers");

            migrationBuilder.DropColumn(
                name: "BackOfficeRoleId",
                table: "BackOfficeUsers");

            migrationBuilder.DropColumn(
                name: "PageName",
                table: "BackOfficeRoles");

            migrationBuilder.DropColumn(
                name: "PageUrl",
                table: "BackOfficeRoles");
        }
    }
}
