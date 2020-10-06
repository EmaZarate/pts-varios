using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class propertiescorrectiveactionfishbone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobsSectorsPlants_SectorID_PlantID",
                table: "JobsSectorsPlants");

            migrationBuilder.RenameColumn(
                name: "YPos",
                table: "CorrectiveActionFishboneCauses",
                newName: "Y2");

            migrationBuilder.RenameColumn(
                name: "XPos",
                table: "CorrectiveActionFishboneCauses",
                newName: "Y1");

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "CorrectiveActionFishboneCauseWhys",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SubChildren",
                table: "CorrectiveActionFishboneCauseWhys",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "X1",
                table: "CorrectiveActionFishboneCauses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "X2",
                table: "CorrectiveActionFishboneCauses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobsSectorsPlants_PlantID_SectorID",
                table: "JobsSectorsPlants",
                columns: new[] { "PlantID", "SectorID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobsSectorsPlants_PlantID_SectorID",
                table: "JobsSectorsPlants");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "CorrectiveActionFishboneCauseWhys");

            migrationBuilder.DropColumn(
                name: "SubChildren",
                table: "CorrectiveActionFishboneCauseWhys");

            migrationBuilder.DropColumn(
                name: "X1",
                table: "CorrectiveActionFishboneCauses");

            migrationBuilder.DropColumn(
                name: "X2",
                table: "CorrectiveActionFishboneCauses");

            migrationBuilder.RenameColumn(
                name: "Y2",
                table: "CorrectiveActionFishboneCauses",
                newName: "YPos");

            migrationBuilder.RenameColumn(
                name: "Y1",
                table: "CorrectiveActionFishboneCauses",
                newName: "XPos");

            migrationBuilder.CreateIndex(
                name: "IX_JobsSectorsPlants_SectorID_PlantID",
                table: "JobsSectorsPlants",
                columns: new[] { "SectorID", "PlantID" });
        }
    }
}
