using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class initialss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceName",
                table: "VendorProcessServices");

            migrationBuilder.AlterColumn<string>(
                name: "ServiceTypeName",
                table: "VendorProcessServiceTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "VendorProcessServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerRequestServiceId",
                table: "VendorProcessServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CustomerRequestServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRequestServices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VendorProcessServices_CustomerRequestServiceId",
                table: "VendorProcessServices",
                column: "CustomerRequestServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorProcessServices_CustomerRequestServices_CustomerRequestServiceId",
                table: "VendorProcessServices",
                column: "CustomerRequestServiceId",
                principalTable: "CustomerRequestServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorProcessServices_CustomerRequestServices_CustomerRequestServiceId",
                table: "VendorProcessServices");

            migrationBuilder.DropTable(
                name: "CustomerRequestServices");

            migrationBuilder.DropIndex(
                name: "IX_VendorProcessServices_CustomerRequestServiceId",
                table: "VendorProcessServices");

            migrationBuilder.DropColumn(
                name: "CustomerRequestServiceId",
                table: "VendorProcessServices");

            migrationBuilder.AlterColumn<string>(
                name: "ServiceTypeName",
                table: "VendorProcessServiceTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "VendorProcessServices",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                table: "VendorProcessServices",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
