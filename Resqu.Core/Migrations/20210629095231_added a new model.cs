using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class addedanewmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceToSericeCategorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: true),
                    CustomerRequestServiceId = table.Column<int>(type: "int", nullable: true),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: true),
                    VendorProcessServiceTypeId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceToSericeCategorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceToSericeCategorys_CustomerRequestServices_CustomerRequestServiceId",
                        column: x => x.CustomerRequestServiceId,
                        principalTable: "CustomerRequestServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceToSericeCategorys_VendorProcessServiceTypes_VendorProcessServiceTypeId",
                        column: x => x.VendorProcessServiceTypeId,
                        principalTable: "VendorProcessServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceToSericeCategorys_CustomerRequestServiceId",
                table: "ServiceToSericeCategorys",
                column: "CustomerRequestServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceToSericeCategorys_VendorProcessServiceTypeId",
                table: "ServiceToSericeCategorys",
                column: "VendorProcessServiceTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceToSericeCategorys");
        }
    }
}
