using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class initialers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "VendorProcessServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "VendorProcessServices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "VendorProcessServices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBan",
                table: "VendorProcessServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "VendorProcessServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFullyVerified",
                table: "VendorProcessServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsModified",
                table: "VendorProcessServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isVerified",
                table: "VendorProcessServices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "VendorProcessServices");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "VendorProcessServices");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "VendorProcessServices");

            migrationBuilder.DropColumn(
                name: "IsBan",
                table: "VendorProcessServices");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "VendorProcessServices");

            migrationBuilder.DropColumn(
                name: "IsFullyVerified",
                table: "VendorProcessServices");

            migrationBuilder.DropColumn(
                name: "IsModified",
                table: "VendorProcessServices");

            migrationBuilder.DropColumn(
                name: "isVerified",
                table: "VendorProcessServices");
        }
    }
}
