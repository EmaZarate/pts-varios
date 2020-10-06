using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class addtableUserCorrectiveAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCorrectiveActions",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    CorrectiveActionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCorrectiveActions", x => new { x.UserID, x.CorrectiveActionID });
                    table.ForeignKey(
                        name: "FK_UserCorrectiveActions_CorrectiveActions_CorrectiveActionID",
                        column: x => x.CorrectiveActionID,
                        principalTable: "CorrectiveActions",
                        principalColumn: "CorrectiveActionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCorrectiveActions_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCorrectiveActions_CorrectiveActionID",
                table: "UserCorrectiveActions",
                column: "CorrectiveActionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCorrectiveActions");
        }
    }
}
