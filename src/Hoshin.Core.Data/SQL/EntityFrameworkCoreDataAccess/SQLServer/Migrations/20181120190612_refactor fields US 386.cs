using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class refactorfieldsUS386 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SectorsPlants_Plants_PlantID",
                table: "SectorsPlants");

            migrationBuilder.RenameColumn(
                name: "Nomenclature",
                table: "Jobs",
                newName: "Code");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Sectors",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Plants",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Plants",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "Jobs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_SectorsPlants_Plants_PlantID",
                table: "SectorsPlants",
                column: "PlantID",
                principalTable: "Plants",
                principalColumn: "PlantID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SectorsPlants_Plants_PlantID",
                table: "SectorsPlants");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Sectors");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Jobs",
                newName: "Nomenclature");

            migrationBuilder.AlterColumn<string>(
                name: "Active",
                table: "Jobs",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddForeignKey(
                name: "FK_SectorsPlants_Plants_PlantID",
                table: "SectorsPlants",
                column: "PlantID",
                principalTable: "Plants",
                principalColumn: "PlantID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
