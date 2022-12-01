using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElsaEdiBackend.Migrations.AppDb
{
    public partial class RemoveCreatedOnAccessList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "permission_createdon",
                table: "AccessList");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AccessList",
                newName: "permission_userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "permission_userid",
                table: "AccessList",
                newName: "UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "permission_createdon",
                table: "AccessList",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
