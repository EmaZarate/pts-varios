using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class FixAspectState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditStandardAspects_AuditStates_AuditStateID",
                table: "AuditStandardAspects");

            migrationBuilder.RenameColumn(
                name: "AuditStateID",
                table: "AuditStandardAspects",
                newName: "AspectStateID");

            migrationBuilder.RenameIndex(
                name: "IX_AuditStandardAspects_AuditStateID",
                table: "AuditStandardAspects",
                newName: "IX_AuditStandardAspects_AspectStateID");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditStandardAspects_AspectStates_AspectStateID",
                table: "AuditStandardAspects",
                column: "AspectStateID",
                principalTable: "AspectStates",
                principalColumn: "AspectStateID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditStandardAspects_AspectStates_AspectStateID",
                table: "AuditStandardAspects");

            migrationBuilder.RenameColumn(
                name: "AspectStateID",
                table: "AuditStandardAspects",
                newName: "AuditStateID");

            migrationBuilder.RenameIndex(
                name: "IX_AuditStandardAspects_AspectStateID",
                table: "AuditStandardAspects",
                newName: "IX_AuditStandardAspects_AuditStateID");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditStandardAspects_AuditStates_AuditStateID",
                table: "AuditStandardAspects",
                column: "AuditStateID",
                principalTable: "AuditStates",
                principalColumn: "AuditStateID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
