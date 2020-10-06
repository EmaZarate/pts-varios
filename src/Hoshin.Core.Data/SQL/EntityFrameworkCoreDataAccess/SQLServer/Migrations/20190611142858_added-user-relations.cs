using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class addeduserrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectiveActions_AspNetUsers_ResponisbleUserId",
                table: "CorrectiveActions");

            migrationBuilder.DropIndex(
                name: "IX_CorrectiveActions_ResponisbleUserId",
                table: "CorrectiveActions");

            migrationBuilder.DropColumn(
                name: "ResponisbleUserId",
                table: "CorrectiveActions");

            migrationBuilder.AlterColumn<string>(
                name: "ResponsibleUserID",
                table: "CorrectiveActions",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActions_ResponsibleUserID",
                table: "CorrectiveActions",
                column: "ResponsibleUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectiveActions_AspNetUsers_ResponsibleUserID",
                table: "CorrectiveActions",
                column: "ResponsibleUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectiveActions_AspNetUsers_ResponsibleUserID",
                table: "CorrectiveActions");

            migrationBuilder.DropIndex(
                name: "IX_CorrectiveActions_ResponsibleUserID",
                table: "CorrectiveActions");

            migrationBuilder.AlterColumn<string>(
                name: "ResponsibleUserID",
                table: "CorrectiveActions",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponisbleUserId",
                table: "CorrectiveActions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CorrectiveActions_ResponisbleUserId",
                table: "CorrectiveActions",
                column: "ResponisbleUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectiveActions_AspNetUsers_ResponisbleUserId",
                table: "CorrectiveActions",
                column: "ResponisbleUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
