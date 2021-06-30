using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class addedrelationshiptoissuemode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IssueDescription",
                table: "ResquServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IssueId",
                table: "ResquServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssuePrice",
                table: "ResquServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SubCategoryPrice",
                table: "ResquServices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssueDescription",
                table: "ResquServices");

            migrationBuilder.DropColumn(
                name: "IssueId",
                table: "ResquServices");

            migrationBuilder.DropColumn(
                name: "IssuePrice",
                table: "ResquServices");

            migrationBuilder.DropColumn(
                name: "SubCategoryPrice",
                table: "ResquServices");
        }
    }
}
