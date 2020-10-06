using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class InitialMigrationQualityModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FindingsStates",
                columns: table => new
                {
                    FindingStateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Colour = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FindingsStates", x => x.FindingStateID);
                });

            migrationBuilder.CreateTable(
                name: "FindingTypes",
                columns: table => new
                {
                    FindingTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FindingTypes", x => x.FindingTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ParametrizationCriterias",
                columns: table => new
                {
                    ParametrizationCriteriaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    DataType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametrizationCriterias", x => x.ParametrizationCriteriaID);
                });

            migrationBuilder.CreateTable(
                name: "Findings",
                columns: table => new
                {
                    FindingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CauseAnalysis = table.Column<string>(nullable: true),
                    ContainmentAction = table.Column<string>(nullable: true),
                    EmitterUserID = table.Column<string>(nullable: true),
                    ResponsibleUserID = table.Column<string>(nullable: true),
                    PlantLocationID = table.Column<int>(nullable: false),
                    SectorLocationID = table.Column<int>(nullable: false),
                    PlantTreatmentID = table.Column<int>(nullable: false),
                    SectorTreatmentID = table.Column<int>(nullable: false),
                    FindingTypeID = table.Column<int>(nullable: false),
                    FindingStateID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Findings", x => x.FindingID);
                    table.ForeignKey(
                        name: "FK_Findings_AspNetUsers_EmitterUserID",
                        column: x => x.EmitterUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Findings_FindingsStates_FindingStateID",
                        column: x => x.FindingStateID,
                        principalTable: "FindingsStates",
                        principalColumn: "FindingStateID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Findings_FindingTypes_FindingTypeID",
                        column: x => x.FindingTypeID,
                        principalTable: "FindingTypes",
                        principalColumn: "FindingTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Findings_AspNetUsers_ResponsibleUserID",
                        column: x => x.ResponsibleUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Findings_SectorsPlants_PlantLocationID_SectorLocationID",
                        columns: x => new { x.PlantLocationID, x.SectorLocationID },
                        principalTable: "SectorsPlants",
                        principalColumns: new[] { "PlantID", "SectorID" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Findings_SectorsPlants_PlantTreatmentID_SectorTreatmentID",
                        columns: x => new { x.PlantTreatmentID, x.SectorTreatmentID },
                        principalTable: "SectorsPlants",
                        principalColumns: new[] { "PlantID", "SectorID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParametrizationsFindingTypes",
                columns: table => new
                {
                    FindingTypeID = table.Column<int>(nullable: false),
                    ParametrizationCriteriaID = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametrizationsFindingTypes", x => new { x.ParametrizationCriteriaID, x.FindingTypeID });
                    table.ForeignKey(
                        name: "FK_ParametrizationsFindingTypes_FindingTypes_FindingTypeID",
                        column: x => x.FindingTypeID,
                        principalTable: "FindingTypes",
                        principalColumn: "FindingTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParametrizationsFindingTypes_ParametrizationCriterias_ParametrizationCriteriaID",
                        column: x => x.ParametrizationCriteriaID,
                        principalTable: "ParametrizationCriterias",
                        principalColumn: "ParametrizationCriteriaID",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "CorrectiveActions",
                columns: table => new
                {
                    CorrectiveActionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FindingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectiveActions", x => x.CorrectiveActionID);
                    table.ForeignKey(
                        name: "FK_CorrectiveActions_Findings_FindingID",
                        column: x => x.FindingID,
                        principalTable: "Findings",
                        principalColumn: "FindingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FindingComments",
                columns: table => new
                {
                    FindingCommentID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    FindingID = table.Column<int>(nullable: false),
                    CreatedByUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FindingComments", x => x.FindingCommentID);
                    table.ForeignKey(
                        name: "FK_FindingComments_Findings_FindingCommentID",
                        column: x => x.FindingCommentID,
                        principalTable: "Findings",
                        principalColumn: "FindingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FindingsEvidences",
                columns: table => new
                {
                    FindingEvidenceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FindingID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FindingsEvidences", x => x.FindingEvidenceID);
                    table.ForeignKey(
                        name: "FK_FindingsEvidences_Findings_FindingID",
                        column: x => x.FindingID,
                        principalTable: "Findings",
                        principalColumn: "FindingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FindingsReassignmentsHistories",
                columns: table => new
                {
                    FindingReassignmentHistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    ReassignedUserID = table.Column<int>(nullable: false),
                    FindingID = table.Column<int>(nullable: false),
                    CreatedByUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FindingsReassignmentsHistories", x => x.FindingReassignmentHistoryID);
                    table.ForeignKey(
                        name: "FK_FindingsReassignmentsHistories_Findings_FindingID",
                        column: x => x.FindingID,
                        principalTable: "Findings",
                        principalColumn: "FindingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FindingsStatesHistories",
                columns: table => new
                {
                    FindingStateHistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    FindingStateID = table.Column<int>(nullable: false),
                    FindingID = table.Column<int>(nullable: false),
                    CreatedByUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FindingsStatesHistories", x => x.FindingStateHistoryID);
                    table.ForeignKey(
                        name: "FK_FindingsStatesHistories_Findings_FindingID",
                        column: x => x.FindingID,
                        principalTable: "Findings",
                        principalColumn: "FindingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FindingsStatesHistories_FindingsStates_FindingStateID",
                        column: x => x.FindingStateID,
                        principalTable: "FindingsStates",
                        principalColumn: "FindingStateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierEvaluations",
                columns: table => new
                {
                    SupplierEvaluationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FindingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierEvaluations", x => x.SupplierEvaluationID);
                    table.ForeignKey(
                        name: "FK_SupplierEvaluations_Findings_FindingID",
                        column: x => x.FindingID,
                        principalTable: "Findings",
                        principalColumn: "FindingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActions_FindingID",
                table: "CorrectiveActions",
                column: "FindingID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Findings_EmitterUserID",
                table: "Findings",
                column: "EmitterUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Findings_FindingStateID",
                table: "Findings",
                column: "FindingStateID");

            migrationBuilder.CreateIndex(
                name: "IX_Findings_FindingTypeID",
                table: "Findings",
                column: "FindingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Findings_ResponsibleUserID",
                table: "Findings",
                column: "ResponsibleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Findings_PlantLocationID_SectorLocationID",
                table: "Findings",
                columns: new[] { "PlantLocationID", "SectorLocationID" });

            migrationBuilder.CreateIndex(
                name: "IX_Findings_PlantTreatmentID_SectorTreatmentID",
                table: "Findings",
                columns: new[] { "PlantTreatmentID", "SectorTreatmentID" });

            migrationBuilder.CreateIndex(
                name: "IX_FindingsEvidences_FindingID",
                table: "FindingsEvidences",
                column: "FindingID");

            migrationBuilder.CreateIndex(
                name: "IX_FindingsReassignmentsHistories_FindingID",
                table: "FindingsReassignmentsHistories",
                column: "FindingID");

            migrationBuilder.CreateIndex(
                name: "IX_FindingsStatesHistories_FindingID",
                table: "FindingsStatesHistories",
                column: "FindingID");

            migrationBuilder.CreateIndex(
                name: "IX_FindingsStatesHistories_FindingStateID",
                table: "FindingsStatesHistories",
                column: "FindingStateID");

            migrationBuilder.CreateIndex(
                name: "IX_ParametrizationsFindingTypes_FindingTypeID",
                table: "ParametrizationsFindingTypes",
                column: "FindingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierEvaluations_FindingID",
                table: "SupplierEvaluations",
                column: "FindingID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditsAspectsFindings");

            migrationBuilder.DropTable(
                name: "CorrectiveActions");

            migrationBuilder.DropTable(
                name: "FindingComments");

            migrationBuilder.DropTable(
                name: "FindingsEvidences");

            migrationBuilder.DropTable(
                name: "FindingsReassignmentsHistories");

            migrationBuilder.DropTable(
                name: "FindingsStatesHistories");

            migrationBuilder.DropTable(
                name: "ParametrizationsFindingTypes");

            migrationBuilder.DropTable(
                name: "SupplierEvaluations");

            migrationBuilder.DropTable(
                name: "AuditsAspects");

            migrationBuilder.DropTable(
                name: "ParametrizationCriterias");

            migrationBuilder.DropTable(
                name: "Findings");

            migrationBuilder.DropTable(
                name: "FindingsStates");

            migrationBuilder.DropTable(
                name: "FindingTypes");
        }
    }
}
