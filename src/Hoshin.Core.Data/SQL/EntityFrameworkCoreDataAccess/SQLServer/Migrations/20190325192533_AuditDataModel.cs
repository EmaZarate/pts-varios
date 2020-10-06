using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class AuditDataModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.DropTable(
                name: "AuditsAspectsFindings");

            migrationBuilder.DropTable(
                name: "AuditsAspects");


            migrationBuilder.CreateTable(
                name: "AspectStates",
                columns: table => new
                {
                    AspectStateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspectStates", x => x.AspectStateID);
                });

            migrationBuilder.CreateTable(
                name: "AuditStates",
                columns: table => new
                {
                    AuditStateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditStates", x => x.AuditStateID);
                });

            migrationBuilder.CreateTable(
                name: "AuditsTypes",
                columns: table => new
                {
                    AuditTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditsTypes", x => x.AuditTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Standards",
                columns: table => new
                {
                    StandardID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Standards", x => x.StandardID);
                });

            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    AuditID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    AuditInitDate = table.Column<DateTime>(nullable: false),
                    AuditStateID = table.Column<int>(nullable: false),
                    AuditTypeID = table.Column<int>(nullable: false),
                    SectorID = table.Column<int>(nullable: false),
                    PlantID = table.Column<int>(nullable: false),
                    AuditorID = table.Column<string>(nullable: true),
                    ExternalAuditor = table.Column<string>(nullable: true),
                    AuditTeam = table.Column<string>(nullable: true),
                    DocumentsAnalysisDate = table.Column<DateTime>(nullable: false),
                    DocumentAnalysisDuration = table.Column<int>(nullable: false),
                    AuditInitTime = table.Column<DateTime>(nullable: false),
                    AuditFinishDate = table.Column<DateTime>(nullable: false),
                    AuditFinishTime = table.Column<DateTime>(nullable: false),
                    CloseMeetingDate = table.Column<DateTime>(nullable: false),
                    CloseMeetingDuration = table.Column<int>(nullable: false),
                    ReportEmittedDate = table.Column<DateTime>(nullable: false),
                    CloseDate = table.Column<DateTime>(nullable: false),
                    Conclusion = table.Column<string>(nullable: true),
                    Recomendations = table.Column<string>(nullable: true),
                    ApprovePlanComments = table.Column<string>(nullable: true),
                    ApproveReportComments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.AuditID);
                    table.ForeignKey(
                        name: "FK_Audits_AuditStates_AuditStateID",
                        column: x => x.AuditStateID,
                        principalTable: "AuditStates",
                        principalColumn: "AuditStateID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Audits_AuditsTypes_AuditTypeID",
                        column: x => x.AuditTypeID,
                        principalTable: "AuditsTypes",
                        principalColumn: "AuditTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Audits_AspNetUsers_AuditorID",
                        column: x => x.AuditorID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Audits_SectorsPlants_SectorID_PlantID",
                        columns: x => new { x.SectorID, x.PlantID },
                        principalTable: "SectorsPlants",
                        principalColumns: new[] { "PlantID", "SectorID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Aspects",
                columns: table => new
                {
                    AspectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    StandardID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aspects", x => x.AspectID);
                    table.ForeignKey(
                        name: "FK_Aspects_Standards_StandardID",
                        column: x => x.StandardID,
                        principalTable: "Standards",
                        principalColumn: "StandardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditReschedulingHistory",
                columns: table => new
                {
                    AuditReschedulingHistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditID = table.Column<int>(nullable: false),
                    DateRescheduling = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditReschedulingHistory", x => x.AuditReschedulingHistoryID);
                    table.ForeignKey(
                        name: "FK_AuditReschedulingHistory_Audits_AuditID",
                        column: x => x.AuditID,
                        principalTable: "Audits",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditStandard",
                columns: table => new
                {
                    AuditID = table.Column<int>(nullable: false),
                    StandardID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditStandard", x => new { x.AuditID, x.StandardID });
                    table.ForeignKey(
                        name: "FK_AuditStandard_Audits_AuditID",
                        column: x => x.AuditID,
                        principalTable: "Audits",
                        principalColumn: "AuditID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuditStandard_Standards_StandardID",
                        column: x => x.StandardID,
                        principalTable: "Standards",
                        principalColumn: "StandardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditStandardAspects",
                columns: table => new
                {
                    AuditID = table.Column<int>(nullable: false),
                    StandardID = table.Column<int>(nullable: false),
                    AspectID = table.Column<int>(nullable: false),
                    AuditStateID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditStandardAspects", x => new { x.AspectID, x.AuditID, x.StandardID });
                    table.ForeignKey(
                        name: "FK_AuditStandardAspects_Aspects_AspectID",
                        column: x => x.AspectID,
                        principalTable: "Aspects",
                        principalColumn: "AspectID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditStandardAspects_AuditStates_AuditStateID",
                        column: x => x.AuditStateID,
                        principalTable: "AuditStates",
                        principalColumn: "AuditStateID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditStandardAspects_AuditStandard_AuditID_StandardID",
                        columns: x => new { x.AuditID, x.StandardID },
                        principalTable: "AuditStandard",
                        principalColumns: new[] { "AuditID", "StandardID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aspects_StandardID",
                table: "Aspects",
                column: "StandardID");

            migrationBuilder.CreateIndex(
                name: "IX_AuditReschedulingHistory_AuditID",
                table: "AuditReschedulingHistory",
                column: "AuditID");

            migrationBuilder.CreateIndex(
                name: "IX_Audits_AuditStateID",
                table: "Audits",
                column: "AuditStateID");

            migrationBuilder.CreateIndex(
                name: "IX_Audits_AuditTypeID",
                table: "Audits",
                column: "AuditTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Audits_AuditorID",
                table: "Audits",
                column: "AuditorID");

            migrationBuilder.CreateIndex(
                name: "IX_Audits_SectorID_PlantID",
                table: "Audits",
                columns: new[] { "SectorID", "PlantID" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditStandard_StandardID",
                table: "AuditStandard",
                column: "StandardID");

            migrationBuilder.CreateIndex(
                name: "IX_AuditStandardAspects_AuditStateID",
                table: "AuditStandardAspects",
                column: "AuditStateID");

            migrationBuilder.CreateIndex(
                name: "IX_AuditStandardAspects_AuditID_StandardID",
                table: "AuditStandardAspects",
                columns: new[] { "AuditID", "StandardID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.DropTable(
                name: "AspectStates");

            migrationBuilder.DropTable(
                name: "AuditReschedulingHistory");

            migrationBuilder.DropTable(
                name: "AuditStandardAspects");

            migrationBuilder.DropTable(
                name: "Aspects");

            migrationBuilder.DropTable(
                name: "AuditStandard");

            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "Standards");

            migrationBuilder.DropTable(
                name: "AuditStates");

            migrationBuilder.DropTable(
                name: "AuditsTypes");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImageProfile",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "base64Profile",
                table: "AspNetUsers");

           

            migrationBuilder.CreateTable(
                name: "AuditsAspects",
                columns: table => new
                {
                    AuditAspectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditsAspects", x => x.AuditAspectID);
                });

            migrationBuilder.CreateTable(
                name: "AuditsAspectsFindings",
                columns: table => new
                {
                    AuditAspectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditsAspectsFindings", x => x.AuditAspectID);
                    table.ForeignKey(
                        name: "FK_AuditsAspectsFindings_AuditsAspects_AuditAspectID",
                        column: x => x.AuditAspectID,
                        principalTable: "AuditsAspects",
                        principalColumn: "AuditAspectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuditsAspectsFindings_Findings_AuditAspectID",
                        column: x => x.AuditAspectID,
                        principalTable: "Findings",
                        principalColumn: "FindingID",
                        onDelete: ReferentialAction.Cascade);
                });

           
        }
    }
}
