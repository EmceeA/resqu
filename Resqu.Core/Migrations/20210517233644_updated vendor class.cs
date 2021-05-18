using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class updatedvendorclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_VendorId",
                table: "Transactions",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Vendors_VendorId",
                table: "Transactions",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Vendors_VendorId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_VendorId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Transactions");
        }
    }
}
