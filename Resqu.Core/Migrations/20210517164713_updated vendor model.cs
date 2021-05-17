using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class updatedvendormodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VendorName",
                table: "Vendors",
                newName: "PhoneNo");

            migrationBuilder.RenameColumn(
                name: "NextOfKin",
                table: "Vendors",
                newName: "NextOfKinRelationship");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExpertiseId",
                table: "Vendors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentificationNumber",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeansOfIdentification",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_ExpertiseId",
                table: "Vendors",
                column: "ExpertiseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Expertises_ExpertiseId",
                table: "Vendors",
                column: "ExpertiseId",
                principalTable: "Expertises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Expertises_ExpertiseId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_ExpertiseId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "ExpertiseId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "IdentificationNumber",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "MeansOfIdentification",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Vendors");

            migrationBuilder.RenameColumn(
                name: "PhoneNo",
                table: "Vendors",
                newName: "VendorName");

            migrationBuilder.RenameColumn(
                name: "NextOfKinRelationship",
                table: "Vendors",
                newName: "NextOfKin");
        }
    }
}
