using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElsaEdiBackend.Migrations.AppDb
{
    public partial class FixErrorPermissionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AccessList",
                table: "AccessList");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AccessList",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccessList",
                table: "AccessList",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AccessList_WorkflowDefinitionId",
                table: "AccessList",
                column: "WorkflowDefinitionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AccessList",
                table: "AccessList");

            migrationBuilder.DropIndex(
                name: "IX_AccessList_WorkflowDefinitionId",
                table: "AccessList");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AccessList",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccessList",
                table: "AccessList",
                columns: new[] { "WorkflowDefinitionId", "Id" });
        }
    }
}
