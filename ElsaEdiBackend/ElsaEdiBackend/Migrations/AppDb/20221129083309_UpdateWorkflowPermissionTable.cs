using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElsaEdiBackend.Migrations.AppDb
{
    public partial class UpdateWorkflowPermissionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AccessList",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AccessList");
        }
    }
}
