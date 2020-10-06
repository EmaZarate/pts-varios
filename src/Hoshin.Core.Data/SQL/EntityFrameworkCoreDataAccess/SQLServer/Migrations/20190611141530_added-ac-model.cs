using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class addedacmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectiveActions_Findings_FindingID",
                table: "CorrectiveActions");

            migrationBuilder.DropIndex(
                name: "IX_CorrectiveActions_FindingID",
                table: "CorrectiveActions");

            migrationBuilder.AlterColumn<int>(
                name: "FindingID",
                table: "CorrectiveActions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CorrectiveActionStateID",
                table: "CorrectiveActions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "CorrectiveActions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CorrectiveActions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveDateImplementation",
                table: "CorrectiveActions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EvaluationCommentary",
                table: "CorrectiveActions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impact",
                table: "CorrectiveActions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MaxDateEfficiencyEvaluation",
                table: "CorrectiveActions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "MaxDateImplementation",
                table: "CorrectiveActions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PlantEmitterID",
                table: "CorrectiveActions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlantLocationID",
                table: "CorrectiveActions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlantTreatmentID",
                table: "CorrectiveActions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponisbleUserId",
                table: "CorrectiveActions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsibleUserID",
                table: "CorrectiveActions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReviewerUserID",
                table: "CorrectiveActions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SectorEmitterID",
                table: "CorrectiveActions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SectorLocationID",
                table: "CorrectiveActions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SectorTreatmentID",
                table: "CorrectiveActions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkGroup",
                table: "CorrectiveActions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dateTimeEfficiencyEvaluation",
                table: "CorrectiveActions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isEffective",
                table: "CorrectiveActions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CorrectiveActionEvidences",
                columns: table => new
                {
                    CorrectiveActionEvidenceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CorrectiveActionID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectiveActionEvidences", x => x.CorrectiveActionEvidenceID);
                    table.ForeignKey(
                        name: "FK_CorrectiveActionEvidences_CorrectiveActions_CorrectiveActionID",
                        column: x => x.CorrectiveActionID,
                        principalTable: "CorrectiveActions",
                        principalColumn: "CorrectiveActionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CorrectiveActionStates",
                columns: table => new
                {
                    CorrectiveActionStateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectiveActionStates", x => x.CorrectiveActionStateID);
                });

            migrationBuilder.CreateTable(
                name: "Fishbone",
                columns: table => new
                {
                    FishboneID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Color = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fishbone", x => x.FishboneID);
                });

            migrationBuilder.CreateTable(
                name: "TaskStates",
                columns: table => new
                {
                    TaskStateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStates", x => x.TaskStateID);
                });

            migrationBuilder.CreateTable(
                name: "CorrectiveActionFishbone",
                columns: table => new
                {
                    FishboneID = table.Column<int>(nullable: false),
                    CorrectiveActionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectiveActionFishbone", x => new { x.CorrectiveActionID, x.FishboneID });
                    table.ForeignKey(
                        name: "FK_CorrectiveActionFishbone_CorrectiveActions_CorrectiveActionID",
                        column: x => x.CorrectiveActionID,
                        principalTable: "CorrectiveActions",
                        principalColumn: "CorrectiveActionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CorrectiveActionFishbone_Fishbone_FishboneID",
                        column: x => x.FishboneID,
                        principalTable: "Fishbone",
                        principalColumn: "FishboneID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CorrectiveActionTasks",
                columns: table => new
                {
                    CorrectiveActionTaskID = table.Column<int>(nullable: false),
                    CorrectiveActionID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ResponsibleUserID = table.Column<string>(nullable: true),
                    ImplementationPlannedDate = table.Column<DateTime>(nullable: false),
                    ImplementationEffectiveDate = table.Column<DateTime>(nullable: false),
                    Observation = table.Column<string>(nullable: true),
                    Result = table.Column<string>(nullable: true),
                    TaskStateID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectiveActionTasks", x => new { x.CorrectiveActionTaskID, x.CorrectiveActionID });
                    table.ForeignKey(
                        name: "FK_CorrectiveActionTasks_CorrectiveActions_CorrectiveActionID",
                        column: x => x.CorrectiveActionID,
                        principalTable: "CorrectiveActions",
                        principalColumn: "CorrectiveActionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CorrectiveActionTasks_AspNetUsers_ResponsibleUserID",
                        column: x => x.ResponsibleUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CorrectiveActionTasks_TaskStates_TaskStateID",
                        column: x => x.TaskStateID,
                        principalTable: "TaskStates",
                        principalColumn: "TaskStateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CorrectiveActionFishboneCauses",
                columns: table => new
                {
                    CorrectiveActionFishboneCauseID = table.Column<int>(nullable: false),
                    FishboneID = table.Column<int>(nullable: false),
                    CorrectiveActionID = table.Column<int>(nullable: false),
                    XPos = table.Column<int>(nullable: false),
                    YPos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectiveActionFishboneCauses", x => new { x.CorrectiveActionID, x.FishboneID, x.CorrectiveActionFishboneCauseID });
                    table.ForeignKey(
                        name: "FK_CorrectiveActionFishboneCauses_CorrectiveActionFishbone_CorrectiveActionID_FishboneID",
                        columns: x => new { x.CorrectiveActionID, x.FishboneID },
                        principalTable: "CorrectiveActionFishbone",
                        principalColumns: new[] { "CorrectiveActionID", "FishboneID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CorrectiveActionTaskEvidences",
                columns: table => new
                {
                    CorrectiveActionTaskEvidencesID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CorrectiveActionTaskID = table.Column<int>(nullable: false),
                    CorrectiveActionID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectiveActionTaskEvidences", x => x.CorrectiveActionTaskEvidencesID);
                    table.ForeignKey(
                        name: "FK_CorrectiveActionTaskEvidences_CorrectiveActionTasks_CorrectiveActionTaskID_CorrectiveActionID",
                        columns: x => new { x.CorrectiveActionTaskID, x.CorrectiveActionID },
                        principalTable: "CorrectiveActionTasks",
                        principalColumns: new[] { "CorrectiveActionTaskID", "CorrectiveActionID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CorrectiveActionFishboneCauseWhys",
                columns: table => new
                {
                    CorrectiveActionFishboneCauseWhyID = table.Column<int>(nullable: false),
                    CorrectiveActionFishboneCauseID = table.Column<int>(nullable: false),
                    CorrectiveActionID = table.Column<int>(nullable: false),
                    FishboneID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectiveActionFishboneCauseWhys", x => new { x.CorrectiveActionID, x.FishboneID, x.CorrectiveActionFishboneCauseID, x.CorrectiveActionFishboneCauseWhyID });
                    table.ForeignKey(
                        name: "FK_CorrectiveActionFishboneCauseWhys_CorrectiveActionFishboneCauses_CorrectiveActionID_FishboneID_CorrectiveActionFishboneCause~",
                        columns: x => new { x.CorrectiveActionID, x.FishboneID, x.CorrectiveActionFishboneCauseID },
                        principalTable: "CorrectiveActionFishboneCauses",
                        principalColumns: new[] { "CorrectiveActionID", "FishboneID", "CorrectiveActionFishboneCauseID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActions_CorrectiveActionStateID",
                table: "CorrectiveActions",
                column: "CorrectiveActionStateID");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActions_FindingID",
                table: "CorrectiveActions",
                column: "FindingID",
                unique: true,
                filter: "[FindingID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActions_ResponisbleUserId",
                table: "CorrectiveActions",
                column: "ResponisbleUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActions_ReviewerUserID",
                table: "CorrectiveActions",
                column: "ReviewerUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActions_PlantEmitterID_SectorEmitterID",
                table: "CorrectiveActions",
                columns: new[] { "PlantEmitterID", "SectorEmitterID" });

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActions_PlantLocationID_SectorLocationID",
                table: "CorrectiveActions",
                columns: new[] { "PlantLocationID", "SectorLocationID" });

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActions_PlantTreatmentID_SectorTreatmentID",
                table: "CorrectiveActions",
                columns: new[] { "PlantTreatmentID", "SectorTreatmentID" });

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActionEvidences_CorrectiveActionID",
                table: "CorrectiveActionEvidences",
                column: "CorrectiveActionID");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActionFishbone_FishboneID",
                table: "CorrectiveActionFishbone",
                column: "FishboneID");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActionTaskEvidences_CorrectiveActionTaskID_CorrectiveActionID",
                table: "CorrectiveActionTaskEvidences",
                columns: new[] { "CorrectiveActionTaskID", "CorrectiveActionID" });

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActionTasks_CorrectiveActionID",
                table: "CorrectiveActionTasks",
                column: "CorrectiveActionID");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActionTasks_ResponsibleUserID",
                table: "CorrectiveActionTasks",
                column: "ResponsibleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActionTasks_TaskStateID",
                table: "CorrectiveActionTasks",
                column: "TaskStateID");

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectiveActions_CorrectiveActionStates_CorrectiveActionStateID",
                table: "CorrectiveActions",
                column: "CorrectiveActionStateID",
                principalTable: "CorrectiveActionStates",
                principalColumn: "CorrectiveActionStateID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectiveActions_Findings_FindingID",
                table: "CorrectiveActions",
                column: "FindingID",
                principalTable: "Findings",
                principalColumn: "FindingID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectiveActions_AspNetUsers_ResponisbleUserId",
                table: "CorrectiveActions",
                column: "ResponisbleUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectiveActions_AspNetUsers_ReviewerUserID",
                table: "CorrectiveActions",
                column: "ReviewerUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectiveActions_SectorsPlants_PlantEmitterID_SectorEmitterID",
                table: "CorrectiveActions",
                columns: new[] { "PlantEmitterID", "SectorEmitterID" },
                principalTable: "SectorsPlants",
                principalColumns: new[] { "PlantID", "SectorID" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectiveActions_SectorsPlants_PlantLocationID_SectorLocationID",
                table: "CorrectiveActions",
                columns: new[] { "PlantLocationID", "SectorLocationID" },
                principalTable: "SectorsPlants",
                principalColumns: new[] { "PlantID", "SectorID" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectiveActions_SectorsPlants_PlantTreatmentID_SectorTreatmentID",
                table: "CorrectiveActions",
                columns: new[] { "PlantTreatmentID", "SectorTreatmentID" },
                principalTable: "SectorsPlants",
                principalColumns: new[] { "PlantID", "SectorID" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectiveActions_CorrectiveActionStates_CorrectiveActionStateID",
                table: "CorrectiveActions");

            migrationBuilder.DropForeignKey(
                name: "FK_CorrectiveActions_Findings_FindingID",
                table: "CorrectiveActions");

            migrationBuilder.DropForeignKey(
                name: "FK_CorrectiveActions_AspNetUsers_ResponisbleUserId",
                table: "CorrectiveActions");

            migrationBuilder.DropForeignKey(
                name: "FK_CorrectiveActions_AspNetUsers_ReviewerUserID",
                table: "CorrectiveActions");

            migrationBuilder.DropForeignKey(
                name: "FK_CorrectiveActions_SectorsPlants_PlantEmitterID_SectorEmitterID",
                table: "CorrectiveActions");

            migrationBuilder.DropForeignKey(
                name: "FK_CorrectiveActions_SectorsPlants_PlantLocationID_SectorLocationID",
                table: "CorrectiveActions");

            migrationBuilder.DropForeignKey(
                name: "FK_CorrectiveActions_SectorsPlants_PlantTreatmentID_SectorTreatmentID",
                table: "CorrectiveActions");

            migrationBuilder.DropTable(
                name: "CorrectiveActionEvidences");

            migrationBuilder.DropTable(
                name: "CorrectiveActionFishboneCauseWhys");

            migrationBuilder.DropTable(
                name: "CorrectiveActionStates");

            migrationBuilder.DropTable(
                name: "CorrectiveActionTaskEvidences");

            migrationBuilder.DropTable(
                name: "CorrectiveActionFishboneCauses");

            migrationBuilder.DropTable(
                name: "CorrectiveActionTasks");

            migrationBuilder.DropTable(
                name: "CorrectiveActionFishbone");

            migrationBuilder.DropTable(
                name: "TaskStates");

            migrationBuilder.DropTable(
                name: "Fishbone");

            migrationBuilder.DropIndex(
                name: "IX_CorrectiveActions_CorrectiveActionStateID",
                table: "CorrectiveActions");

            migrationBuilder.DropIndex(
                name: "IX_CorrectiveActions_FindingID",
                table: "CorrectiveActions");

            migrationBuilder.DropIndex(
                name: "IX_CorrectiveActions_ResponisbleUserId",
                table: "CorrectiveActions");

            migrationBuilder.DropIndex(
                name: "IX_CorrectiveActions_ReviewerUserID",
                table: "CorrectiveActions");

            migrationBuilder.DropIndex(
                name: "IX_CorrectiveActions_PlantEmitterID_SectorEmitterID",
                table: "CorrectiveActions");

            migrationBuilder.DropIndex(
                name: "IX_CorrectiveActions_PlantLocationID_SectorLocationID",
                table: "CorrectiveActions");

            migrationBuilder.DropIndex(
                name: "IX_CorrectiveActions_PlantTreatmentID_SectorTreatmentID",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "CorrectiveActionStateID",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "EffectiveDateImplementation",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "EvaluationCommentary",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "Impact",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "MaxDateEfficiencyEvaluation",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "MaxDateImplementation",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "PlantEmitterID",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "PlantLocationID",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "PlantTreatmentID",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "ResponisbleUserId",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "ResponsibleUserID",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "ReviewerUserID",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "SectorEmitterID",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "SectorLocationID",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "SectorTreatmentID",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "WorkGroup",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "dateTimeEfficiencyEvaluation",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "isEffective",
                table: "CorrectiveActions");

            migrationBuilder.AlterColumn<int>(
                name: "FindingID",
                table: "CorrectiveActions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActions_FindingID",
                table: "CorrectiveActions",
                column: "FindingID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectiveActions_Findings_FindingID",
                table: "CorrectiveActions",
                column: "FindingID",
                principalTable: "Findings",
                principalColumn: "FindingID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
