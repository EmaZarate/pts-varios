using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class addCorrectiveActionStateHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CorrectiveActionStatesHistory",
                columns: table => new
                {
                    CorrectiveActionStatesHistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    CorrectiveActionStateID = table.Column<int>(nullable: false),
                    CorrectiveActionID = table.Column<int>(nullable: false),
                    CreatedByUserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectiveActionStatesHistory", x => x.CorrectiveActionStatesHistoryID);
                    table.ForeignKey(
                        name: "FK_CorrectiveActionStatesHistory_CorrectiveActions_CorrectiveActionID",
                        column: x => x.CorrectiveActionID,
                        principalTable: "CorrectiveActions",
                        principalColumn: "CorrectiveActionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActionStatesHistory_CorrectiveActionID",
                table: "CorrectiveActionStatesHistory",
                column: "CorrectiveActionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorrectiveActionStatesHistory");
        }
    }
}
