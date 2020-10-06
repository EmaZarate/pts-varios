using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class changerelationshipjspuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_JobsSectorsPlants_SectorID_PlantID_JobID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SectorID_PlantID_JobID",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_JobID_PlantID_SectorID",
                table: "AspNetUsers",
                columns: new[] { "JobID", "PlantID", "SectorID" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_JobsSectorsPlants_JobID_PlantID_SectorID",
                table: "AspNetUsers",
                columns: new[] { "JobID", "PlantID", "SectorID" },
                principalTable: "JobsSectorsPlants",
                principalColumns: new[] { "JobID", "PlantID", "SectorID" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_JobsSectorsPlants_JobID_PlantID_SectorID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_JobID_PlantID_SectorID",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SectorID_PlantID_JobID",
                table: "AspNetUsers",
                columns: new[] { "SectorID", "PlantID", "JobID" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_JobsSectorsPlants_SectorID_PlantID_JobID",
                table: "AspNetUsers",
                columns: new[] { "SectorID", "PlantID", "JobID" },
                principalTable: "JobsSectorsPlants",
                principalColumns: new[] { "JobID", "PlantID", "SectorID" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
