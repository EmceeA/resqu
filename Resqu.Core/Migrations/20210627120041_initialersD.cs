using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class initialersD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Expertises_ExpertiseId",
                table: "Vendors");

            migrationBuilder.RenameColumn(
                name: "ExpertiseId",
                table: "Vendors",
                newName: "CustomerRequestServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Vendors_ExpertiseId",
                table: "Vendors",
                newName: "IX_Vendors_CustomerRequestServiceId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CustomerRequestServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_CustomerRequestServices_CustomerRequestServiceId",
                table: "Vendors",
                column: "CustomerRequestServiceId",
                principalTable: "CustomerRequestServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_CustomerRequestServices_CustomerRequestServiceId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CustomerRequestServices");

            migrationBuilder.RenameColumn(
                name: "CustomerRequestServiceId",
                table: "Vendors",
                newName: "ExpertiseId");

            migrationBuilder.RenameIndex(
                name: "IX_Vendors_CustomerRequestServiceId",
                table: "Vendors",
                newName: "IX_Vendors_ExpertiseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Expertises_ExpertiseId",
                table: "Vendors",
                column: "ExpertiseId",
                principalTable: "Expertises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
