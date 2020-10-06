using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class AddNewTableAlertRol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlertRol",
                columns: table => new
                {
                    RolID = table.Column<string>(nullable: false),
                    AlertID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertRol", x => new { x.AlertID, x.RolID });
                    table.ForeignKey(
                        name: "FK_AlertRol_Alert_AlertID",
                        column: x => x.AlertID,
                        principalTable: "Alert",
                        principalColumn: "AlertID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlertRol_AspNetRoles_RolID",
                        column: x => x.RolID,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlertRol_RolID",
                table: "AlertRol",
                column: "RolID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlertRol");
        }
    }
}
