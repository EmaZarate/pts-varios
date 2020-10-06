using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class dropcommentstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FindingComments");

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

            migrationBuilder.CreateTable(
                name: "FindingComments",
                columns: table => new
                {
                    FindingCommentID = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    CreatedByUserID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    FindingID = table.Column<int>(nullable: false)
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
        }
    }
}
