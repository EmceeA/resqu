using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class addednew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousBalance",
                table: "CustomerInflows");

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "WalletInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bank",
                table: "WalletInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerCode",
                table: "WalletInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "WalletInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "PaymentHistorys",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "WalletInfos");

            migrationBuilder.DropColumn(
                name: "Bank",
                table: "WalletInfos");

            migrationBuilder.DropColumn(
                name: "CustomerCode",
                table: "WalletInfos");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "WalletInfos");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "PaymentHistorys");

            migrationBuilder.AddColumn<double>(
                name: "PreviousBalance",
                table: "CustomerInflows",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
