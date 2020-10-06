using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class changetasktables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorrectiveActionTaskEvidences");

            migrationBuilder.DropTable(
                name: "CorrectiveActionTasks");

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntityID = table.Column<int>(nullable: false),
                    EntityType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ResponsibleUserID = table.Column<string>(nullable: true),
                    ImplementationPlannedDate = table.Column<DateTime>(nullable: false),
                    ImplementationEffectiveDate = table.Column<DateTime>(nullable: false),
                    Observation = table.Column<string>(nullable: true),
                    Result = table.Column<string>(nullable: true),
                    TaskStateID = table.Column<int>(nullable: false),
                    RequireEvidence = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskID);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_ResponsibleUserID",
                        column: x => x.ResponsibleUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskStates_TaskStateID",
                        column: x => x.TaskStateID,
                        principalTable: "TaskStates",
                        principalColumn: "TaskStateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskEvidences",
                columns: table => new
                {
                    TaskEvidencesID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaskID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEvidences", x => x.TaskEvidencesID);
                    table.ForeignKey(
                        name: "FK_TaskEvidences_Tasks_TaskID",
                        column: x => x.TaskID,
                        principalTable: "Tasks",
                        principalColumn: "TaskID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskEvidences_TaskID",
                table: "TaskEvidences",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ResponsibleUserID",
                table: "Tasks",
                column: "ResponsibleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskStateID",
                table: "Tasks",
                column: "TaskStateID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskEvidences");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.CreateTable(
                name: "CorrectiveActionTasks",
                columns: table => new
                {
                    CorrectiveActionTaskID = table.Column<int>(nullable: false),
                    CorrectiveActionID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImplementationEffectiveDate = table.Column<DateTime>(nullable: false),
                    ImplementationPlannedDate = table.Column<DateTime>(nullable: false),
                    Observation = table.Column<string>(nullable: true),
                    ResponsibleUserID = table.Column<string>(nullable: true),
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
                name: "CorrectiveActionTaskEvidences",
                columns: table => new
                {
                    CorrectiveActionTaskEvidencesID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CorrectiveActionID = table.Column<int>(nullable: false),
                    CorrectiveActionTaskID = table.Column<int>(nullable: false),
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
        }
    }
}
