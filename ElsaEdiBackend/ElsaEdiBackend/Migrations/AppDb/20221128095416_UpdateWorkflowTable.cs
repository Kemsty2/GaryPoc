using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElsaEdiBackend.Migrations.AppDb
{
    public partial class UpdateWorkflowTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "WorkflowDefinitions",
                type: "TEXT",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WorkflowDefinitions",
                type: "TEXT",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "DefinitionId",
                table: "WorkflowDefinitions",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinitions_CreatedBy",
                table: "WorkflowDefinitions",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinitions_DefinitionId",
                table: "WorkflowDefinitions",
                column: "DefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinitions_LastModifiedBy",
                table: "WorkflowDefinitions",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinitions_Tag",
                table: "WorkflowDefinitions",
                column: "Tag");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinitions_TenantId",
                table: "WorkflowDefinitions",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkflowDefinitions_CreatedBy",
                table: "WorkflowDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowDefinitions_DefinitionId",
                table: "WorkflowDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowDefinitions_LastModifiedBy",
                table: "WorkflowDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowDefinitions_Tag",
                table: "WorkflowDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowDefinitions_TenantId",
                table: "WorkflowDefinitions");

            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "WorkflowDefinitions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WorkflowDefinitions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DefinitionId",
                table: "WorkflowDefinitions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 255);
        }
    }
}
