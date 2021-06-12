using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ResquServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ServiceId",
                table: "ResquServices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SubCategoryId",
                table: "ResquServices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "SubCategoryName",
                table: "ResquServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VendorGender",
                table: "ResquServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VendorPhone",
                table: "ResquServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ExpertiseCategories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ResquServices");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "ResquServices");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "ResquServices");

            migrationBuilder.DropColumn(
                name: "SubCategoryName",
                table: "ResquServices");

            migrationBuilder.DropColumn(
                name: "VendorGender",
                table: "ResquServices");

            migrationBuilder.DropColumn(
                name: "VendorPhone",
                table: "ResquServices");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ExpertiseCategories");
        }
    }
}
