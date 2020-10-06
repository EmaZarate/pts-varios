using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class somefieldsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FinalComment",
                table: "Findings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkflowId",
                table: "Findings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalComment",
                table: "Findings");

            migrationBuilder.DropColumn(
                name: "WorkflowId",
                table: "Findings");
        }
    }
}
