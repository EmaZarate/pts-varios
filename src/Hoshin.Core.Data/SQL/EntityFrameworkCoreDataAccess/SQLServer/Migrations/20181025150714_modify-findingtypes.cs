using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class modifyfindingtypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "FindingTypes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "FindingTypes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "FindingTypes");

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "FindingTypes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
