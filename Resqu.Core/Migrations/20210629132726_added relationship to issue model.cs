using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class addedrelationshiptoissuemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceTypeId",
                table: "Issues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendorProcessServiceTypeId",
                table: "Issues",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_VendorProcessServiceTypeId",
                table: "Issues",
                column: "VendorProcessServiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_VendorProcessServiceTypes_VendorProcessServiceTypeId",
                table: "Issues",
                column: "VendorProcessServiceTypeId",
                principalTable: "VendorProcessServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_VendorProcessServiceTypes_VendorProcessServiceTypeId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_VendorProcessServiceTypeId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ServiceTypeId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "VendorProcessServiceTypeId",
                table: "Issues");
        }
    }
}
