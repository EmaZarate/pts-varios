using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class AuditsReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audits_SectorsPlants_SectorID_PlantID",
                table: "Audits");

            migrationBuilder.DropIndex(
                name: "IX_Audits_SectorID_PlantID",
                table: "Audits");

            migrationBuilder.CreateIndex(
                name: "IX_Audits_PlantID_SectorID",
                table: "Audits",
                columns: new[] { "PlantID", "SectorID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Audits_SectorsPlants_PlantID_SectorID",
                table: "Audits",
                columns: new[] { "PlantID", "SectorID" },
                principalTable: "SectorsPlants",
                principalColumns: new[] { "PlantID", "SectorID" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audits_SectorsPlants_PlantID_SectorID",
                table: "Audits");

            migrationBuilder.DropIndex(
                name: "IX_Audits_PlantID_SectorID",
                table: "Audits");

            migrationBuilder.CreateIndex(
                name: "IX_Audits_SectorID_PlantID",
                table: "Audits",
                columns: new[] { "SectorID", "PlantID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Audits_SectorsPlants_SectorID_PlantID",
                table: "Audits",
                columns: new[] { "SectorID", "PlantID" },
                principalTable: "SectorsPlants",
                principalColumns: new[] { "PlantID", "SectorID" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
