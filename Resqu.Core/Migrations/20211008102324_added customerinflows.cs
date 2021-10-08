using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class addedcustomerinflows : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Narration",
                table: "CustomerInflows",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "CustomerInflows",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionTypeDescription",
                table: "CustomerInflows",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Narration",
                table: "CustomerInflows");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "CustomerInflows");

            migrationBuilder.DropColumn(
                name: "TransactionTypeDescription",
                table: "CustomerInflows");
        }
    }
}
