using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class changesectorplantemitterforuseremitter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectiveActions_SectorsPlants_PlantEmitterID_SectorEmitterID",
                table: "CorrectiveActions");

            migrationBuilder.DropIndex(
                name: "IX_CorrectiveActions_PlantEmitterID_SectorEmitterID",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "PlantEmitterID",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "SectorEmitterID",
                table: "CorrectiveActions");

            migrationBuilder.AddColumn<string>(
                name: "EmitterUserID",
                table: "CorrectiveActions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActions_EmitterUserID",
                table: "CorrectiveActions",
                column: "EmitterUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectiveActions_AspNetUsers_EmitterUserID",
                table: "CorrectiveActions",
                column: "EmitterUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectiveActions_AspNetUsers_EmitterUserID",
                table: "CorrectiveActions");

            migrationBuilder.DropIndex(
                name: "IX_CorrectiveActions_EmitterUserID",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "EmitterUserID",
                table: "CorrectiveActions");

            migrationBuilder.AddColumn<int>(
                name: "PlantEmitterID",
                table: "CorrectiveActions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SectorEmitterID",
                table: "CorrectiveActions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActions_PlantEmitterID_SectorEmitterID",
                table: "CorrectiveActions",
                columns: new[] { "PlantEmitterID", "SectorEmitterID" });

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectiveActions_SectorsPlants_PlantEmitterID_SectorEmitterID",
                table: "CorrectiveActions",
                columns: new[] { "PlantEmitterID", "SectorEmitterID" },
                principalTable: "SectorsPlants",
                principalColumns: new[] { "PlantID", "SectorID" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
