namespace ElsaEdiBackend.Domain.WorkflowDefinitions.Dtos
{
    public class WorkflowDefinitionDto
    {
        public string Id { get; set; }
        public string? DefinitionId { get; set; }
        public string? VersionId { get; set; }
        public string? TenantId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Version { get; set; }
        public bool IsPublished { get; set; }
        public bool IsLatest { get; set; }
        public string? Tag { get; set; }
        public ICollection<AccessList>? Permissions { get; set; }
    }
}
