using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class updatedrequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountPerMin",
                table: "ResquServices");

            migrationBuilder.AddColumn<int>(
                name: "ExpertiseCategoryId",
                table: "VendorServiceSubCategories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendorId1",
                table: "VendorServiceSubCategories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVendorAccepted",
                table: "ResquServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVendorRejected",
                table: "ResquServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_VendorServiceSubCategories_ExpertiseCategoryId",
                table: "VendorServiceSubCategories",
                column: "ExpertiseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorServiceSubCategories_VendorId1",
                table: "VendorServiceSubCategories",
                column: "VendorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorServiceSubCategories_ExpertiseCategories_ExpertiseCategoryId",
                table: "VendorServiceSubCategories",
                column: "ExpertiseCategoryId",
                principalTable: "ExpertiseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorServiceSubCategories_Vendors_VendorId1",
                table: "VendorServiceSubCategories",
                column: "VendorId1",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorServiceSubCategories_ExpertiseCategories_ExpertiseCategoryId",
                table: "VendorServiceSubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorServiceSubCategories_Vendors_VendorId1",
                table: "VendorServiceSubCategories");

            migrationBuilder.DropIndex(
                name: "IX_VendorServiceSubCategories_ExpertiseCategoryId",
                table: "VendorServiceSubCategories");

            migrationBuilder.DropIndex(
                name: "IX_VendorServiceSubCategories_VendorId1",
                table: "VendorServiceSubCategories");

            migrationBuilder.DropColumn(
                name: "ExpertiseCategoryId",
                table: "VendorServiceSubCategories");

            migrationBuilder.DropColumn(
                name: "VendorId1",
                table: "VendorServiceSubCategories");

            migrationBuilder.DropColumn(
                name: "IsVendorAccepted",
                table: "ResquServices");

            migrationBuilder.DropColumn(
                name: "IsVendorRejected",
                table: "ResquServices");

            migrationBuilder.AddColumn<decimal>(
                name: "AmountPerMin",
                table: "ResquServices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
