using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class updatedtransactionmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expertises_ExpertiseCategories_ExpertiseCategoryId",
                table: "Expertises");

            migrationBuilder.DropIndex(
                name: "IX_Expertises_ExpertiseCategoryId",
                table: "Expertises");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceDate",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceType",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubCategory",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VendorName",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Expertises",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ExpertiseId",
                table: "ExpertiseCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpertiseCategories_ExpertiseId",
                table: "ExpertiseCategories",
                column: "ExpertiseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertiseCategories_Expertises_ExpertiseId",
                table: "ExpertiseCategories",
                column: "ExpertiseId",
                principalTable: "Expertises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpertiseCategories_Expertises_ExpertiseId",
                table: "ExpertiseCategories");

            migrationBuilder.DropIndex(
                name: "IX_ExpertiseCategories_ExpertiseId",
                table: "ExpertiseCategories");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ServiceDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SubCategory",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "VendorName",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Expertises");

            migrationBuilder.DropColumn(
                name: "ExpertiseId",
                table: "ExpertiseCategories");

            migrationBuilder.CreateIndex(
                name: "IX_Expertises_ExpertiseCategoryId",
                table: "Expertises",
                column: "ExpertiseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expertises_ExpertiseCategories_ExpertiseCategoryId",
                table: "Expertises",
                column: "ExpertiseCategoryId",
                principalTable: "ExpertiseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
