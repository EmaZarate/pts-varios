using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class addrootreasonandimmeadiateactiontoacfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImmediateAction",
                table: "CorrectiveActions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RootReason",
                table: "CorrectiveActions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImmediateAction",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "RootReason",
                table: "CorrectiveActions");
        }
    }
}
