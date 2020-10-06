using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class relation_with_findings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AspectID",
                table: "Findings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuditID",
                table: "Findings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StandardID",
                table: "Findings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Findings_AspectID_AuditID_StandardID",
                table: "Findings",
                columns: new[] { "AspectID", "AuditID", "StandardID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Findings_AuditStandardAspects_AspectID_AuditID_StandardID",
                table: "Findings",
                columns: new[] { "AspectID", "AuditID", "StandardID" },
                principalTable: "AuditStandardAspects",
                principalColumns: new[] { "AspectID", "AuditID", "StandardID" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Findings_AuditStandardAspects_AspectID_AuditID_StandardID",
                table: "Findings");

            migrationBuilder.DropIndex(
                name: "IX_Findings_AspectID_AuditID_StandardID",
                table: "Findings");

            migrationBuilder.DropColumn(
                name: "AspectID",
                table: "Findings");

            migrationBuilder.DropColumn(
                name: "AuditID",
                table: "Findings");

            migrationBuilder.DropColumn(
                name: "StandardID",
                table: "Findings");
        }
    }
}
