using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElsaEdiBackend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
                    Hash = table.Column<string>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    ModelType = table.Column<string>(type: "TEXT", nullable: false),
                    ActivityType = table.Column<string>(type: "TEXT", nullable: false),
                    ActivityId = table.Column<string>(type: "TEXT", nullable: false),
                    WorkflowInstanceId = table.Column<string>(type: "TEXT", nullable: false),
                    CorrelationId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Triggers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
                    Hash = table.Column<string>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    ModelType = table.Column<string>(type: "TEXT", nullable: false),
                    ActivityType = table.Column<string>(type: "TEXT", nullable: false),
                    ActivityId = table.Column<string>(type: "TEXT", nullable: false),
                    WorkflowDefinitionId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triggers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_definition",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DefinitionId = table.Column<string>(type: "TEXT", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSingleton = table.Column<bool>(type: "INTEGER", nullable: false),
                    PersistenceBehavior = table.Column<int>(type: "INTEGER", nullable: false),
                    DeleteCompletedInstances = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPublished = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLatest = table.Column<bool>(type: "INTEGER", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_definition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_instance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DefinitionId = table.Column<string>(type: "TEXT", nullable: false),
                    DefinitionVersionId = table.Column<string>(type: "TEXT", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkflowStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    CorrelationId = table.Column<string>(type: "TEXT", nullable: false),
                    ContextType = table.Column<string>(type: "TEXT", nullable: true),
                    ContextId = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastExecutedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FinishedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CancelledAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FaultedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastExecutedActivityId = table.Column<string>(type: "TEXT", nullable: true),
                    Data = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_instance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowExecutionLogRecords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
                    WorkflowInstanceId = table.Column<string>(type: "TEXT", nullable: false),
                    ActivityId = table.Column<string>(type: "TEXT", nullable: false),
                    ActivityType = table.Column<string>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EventName = table.Column<string>(type: "TEXT", nullable: true),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    Source = table.Column<string>(type: "TEXT", nullable: true),
                    Data = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowExecutionLogRecords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_ActivityId",
                table: "bm",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_ActivityType",
                table: "bm",
                column: "ActivityType");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_ActivityType_TenantId_Hash",
                table: "bm",
                columns: new[] { "ActivityType", "TenantId", "Hash" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_CorrelationId",
                table: "bm",
                column: "CorrelationId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_Hash",
                table: "bm",
                column: "Hash");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_Hash_CorrelationId_TenantId",
                table: "bm",
                columns: new[] { "Hash", "CorrelationId", "TenantId" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_TenantId",
                table: "bm",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_WorkflowInstanceId",
                table: "bm",
                column: "WorkflowInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Trigger_ActivityId",
                table: "Triggers",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Trigger_ActivityType",
                table: "Triggers",
                column: "ActivityType");

            migrationBuilder.CreateIndex(
                name: "IX_Trigger_ActivityType_TenantId_Hash",
                table: "Triggers",
                columns: new[] { "ActivityType", "TenantId", "Hash" });

            migrationBuilder.CreateIndex(
                name: "IX_Trigger_Hash",
                table: "Triggers",
                column: "Hash");

            migrationBuilder.CreateIndex(
                name: "IX_Trigger_Hash_TenantId",
                table: "Triggers",
                columns: new[] { "Hash", "TenantId" });

            migrationBuilder.CreateIndex(
                name: "IX_Trigger_TenantId",
                table: "Triggers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Trigger_WorkflowDefinitionId",
                table: "Triggers",
                column: "WorkflowDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinition_DefinitionId_VersionId",
                table: "wf_definition",
                columns: new[] { "DefinitionId", "Version" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinition_IsLatest",
                table: "wf_definition",
                column: "IsLatest");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinition_IsPublished",
                table: "wf_definition",
                column: "IsPublished");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinition_Name",
                table: "wf_definition",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinition_Tag",
                table: "wf_definition",
                column: "Tag");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinition_TenantId",
                table: "wf_definition",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowDefinition_Version",
                table: "wf_definition",
                column: "Version");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstance_ContextId",
                table: "wf_instance",
                column: "ContextId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstance_ContextType",
                table: "wf_instance",
                column: "ContextType");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstance_CorrelationId",
                table: "wf_instance",
                column: "CorrelationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstance_CreatedAt",
                table: "wf_instance",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstance_DefinitionId",
                table: "wf_instance",
                column: "DefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstance_DefinitionVersionId",
                table: "wf_instance",
                column: "DefinitionVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstance_FaultedAt",
                table: "wf_instance",
                column: "FaultedAt");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstance_FinishedAt",
                table: "wf_instance",
                column: "FinishedAt");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstance_LastExecutedAt",
                table: "wf_instance",
                column: "LastExecutedAt");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstance_Name",
                table: "wf_instance",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstance_TenantId",
                table: "wf_instance",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstance_WorkflowStatus",
                table: "wf_instance",
                column: "WorkflowStatus");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstance_WorkflowStatus_DefinitionId",
                table: "wf_instance",
                columns: new[] { "WorkflowStatus", "DefinitionId" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInstance_WorkflowStatus_DefinitionId_Version",
                table: "wf_instance",
                columns: new[] { "WorkflowStatus", "DefinitionId", "Version" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowExecutionLogRecord_ActivityId",
                table: "WorkflowExecutionLogRecords",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowExecutionLogRecord_ActivityType",
                table: "WorkflowExecutionLogRecords",
                column: "ActivityType");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowExecutionLogRecord_TenantId",
                table: "WorkflowExecutionLogRecords",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowExecutionLogRecord_Timestamp",
                table: "WorkflowExecutionLogRecords",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowExecutionLogRecord_WorkflowInstanceId",
                table: "WorkflowExecutionLogRecords",
                column: "WorkflowInstanceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bm");

            migrationBuilder.DropTable(
                name: "Triggers");

            migrationBuilder.DropTable(
                name: "wf_definition");

            migrationBuilder.DropTable(
                name: "wf_instance");

            migrationBuilder.DropTable(
                name: "WorkflowExecutionLogRecords");
        }
    }
}
