using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElsaEdiBackend.Migrations.AppDb
{
    public partial class UpdateWorkflowInstanceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowInstance_WorkflowDefinitions_WorkflowDefinitionId",
                table: "WorkflowInstance");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "WorkflowInstance",
                newName: "InstanceId");

            migrationBuilder.AlterColumn<string>(
                name: "WorkflowDefinitionId",
                table: "WorkflowInstance",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "ActivityId",
                table: "WorkflowInstance",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorrelationId",
                table: "WorkflowInstance",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WorkflowInstance",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "WorkflowInstance",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "WorkflowInstance",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkflowStatus",
                table: "WorkflowInstance",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowInstance_WorkflowDefinitions_WorkflowDefinitionId",
                table: "WorkflowInstance",
                column: "WorkflowDefinitionId",
                principalTable: "WorkflowDefinitions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowInstance_WorkflowDefinitions_WorkflowDefinitionId",
                table: "WorkflowInstance");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "WorkflowInstance");

            migrationBuilder.DropColumn(
                name: "CorrelationId",
                table: "WorkflowInstance");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WorkflowInstance");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "WorkflowInstance");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "WorkflowInstance");

            migrationBuilder.DropColumn(
                name: "WorkflowStatus",
                table: "WorkflowInstance");

            migrationBuilder.RenameColumn(
                name: "InstanceId",
                table: "WorkflowInstance",
                newName: "Description");

            migrationBuilder.AlterColumn<string>(
                name: "WorkflowDefinitionId",
                table: "WorkflowInstance",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowInstance_WorkflowDefinitions_WorkflowDefinitionId",
                table: "WorkflowInstance",
                column: "WorkflowDefinitionId",
                principalTable: "WorkflowDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
