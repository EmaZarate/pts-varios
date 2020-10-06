using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class addwithoutfindingsnoauditedfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NoAudited",
                table: "AuditStandardAspects",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WithoutFindings",
                table: "AuditStandardAspects",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoAudited",
                table: "AuditStandardAspects");

            migrationBuilder.DropColumn(
                name: "WithoutFindings",
                table: "AuditStandardAspects");
        }
    }
}
