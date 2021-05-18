using Microsoft.EntityFrameworkCore.Migrations;

namespace Resqu.Core.Migrations
{
    public partial class expertiseupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpertiseCategories_Expertises_ExpertiseId",
                table: "ExpertiseCategories");

            migrationBuilder.AlterColumn<int>(
                name: "ExpertiseId",
                table: "ExpertiseCategories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertiseCategories_Expertises_ExpertiseId",
                table: "ExpertiseCategories",
                column: "ExpertiseId",
                principalTable: "Expertises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpertiseCategories_Expertises_ExpertiseId",
                table: "ExpertiseCategories");

            migrationBuilder.AlterColumn<int>(
                name: "ExpertiseId",
                table: "ExpertiseCategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertiseCategories_Expertises_ExpertiseId",
                table: "ExpertiseCategories",
                column: "ExpertiseId",
                principalTable: "Expertises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
