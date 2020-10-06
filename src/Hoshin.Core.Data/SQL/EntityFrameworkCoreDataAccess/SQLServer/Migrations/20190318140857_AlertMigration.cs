using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class AlertMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alert",
                columns: table => new
                {
                    AlertID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    AlertType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alert", x => x.AlertID);
                });

            migrationBuilder.CreateTable(
                name: "AlertUsers",
                columns: table => new
                {
                    AlertUsersID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GenerateAlert = table.Column<bool>(nullable: false),
                    AlertID = table.Column<int>(nullable: true),
                    UsersId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertUsers", x => x.AlertUsersID);
                    table.ForeignKey(
                        name: "FK_AlertUsers_Alert_AlertID",
                        column: x => x.AlertID,
                        principalTable: "Alert",
                        principalColumn: "AlertID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlertUsers_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlertUsers_AlertID",
                table: "AlertUsers",
                column: "AlertID");

            migrationBuilder.CreateIndex(
                name: "IX_AlertUsers_UsersId",
                table: "AlertUsers",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlertUsers");

            migrationBuilder.DropTable(
                name: "Alert");
        }
    }
}
