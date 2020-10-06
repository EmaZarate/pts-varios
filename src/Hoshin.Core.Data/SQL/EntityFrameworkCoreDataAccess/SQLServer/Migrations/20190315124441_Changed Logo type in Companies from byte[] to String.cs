using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    public partial class ChangedLogotypeinCompaniesfrombytetoString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "Companies",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Logo",
                table: "Companies",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
