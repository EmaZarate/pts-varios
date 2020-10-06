using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class ChangesQualityModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FindingsStatesHistories_FindingsStates_FindingStateID",
                table: "FindingsStatesHistories");

            migrationBuilder.DropIndex(
                name: "IX_FindingsStatesHistories_FindingStateID",
                table: "FindingsStatesHistories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FindingsStatesHistories_FindingStateID",
                table: "FindingsStatesHistories",
                column: "FindingStateID");

            migrationBuilder.AddForeignKey(
                name: "FK_FindingsStatesHistories_FindingsStates_FindingStateID",
                table: "FindingsStatesHistories",
                column: "FindingStateID",
                principalTable: "FindingsStates",
                principalColumn: "FindingStateID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
