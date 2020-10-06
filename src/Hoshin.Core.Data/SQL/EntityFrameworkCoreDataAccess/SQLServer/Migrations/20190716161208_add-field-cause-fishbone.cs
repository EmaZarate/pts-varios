using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class addfieldcausefishbone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CorrectiveActionFishboneCauses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "CorrectiveActionFishboneCauses");
        }
    }
}
