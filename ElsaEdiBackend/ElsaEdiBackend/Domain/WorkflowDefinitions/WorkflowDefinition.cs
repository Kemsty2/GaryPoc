using ElsaEdiBackend.Domain.WorkflowInstances;
using Sieve.Attributes;
using System.Text.Json.Serialization;

namespace ElsaEdiBackend.Domain.WorkflowDefinitions
{
    public class WorkflowDefinition : BaseEntity
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public string? Name { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string? Tag { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public bool IsActive { get; set; }

        [Sieve(CanFilter = true)]
        public string? DefinitionId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string? VersionId { get; set; }

        [Sieve(CanFilter = true)]
        public string? TenantId { get; set; }

        public string? Description { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public int Version { get; set; }

        [Sieve(CanFilter = true)]
        public bool IsPublished { get; set; }

        [Sieve(CanFilter = true)]
        public bool IsLatest { get; set; }

        public virtual ICollection<AccessList> Permissions { get; set; }

        [JsonIgnore]
        public List<WorkflowInstance> WorkflowInstances { get; set; }

        public WorkflowDefinition()
        { } // For EF + Mocking

        internal void UpdatePublishedStatus(bool isPublished)
        {
            IsPublished = isPublished;
        }

        public WorkflowDefinition Update(Elsa.Models.WorkflowDefinition updateDto)
        {
            Name = updateDto.Name;
            Tag = updateDto.Tag;
            VersionId = updateDto.VersionId;
            Version = updateDto.Version;
            Description = updateDto.Description;
            TenantId = updateDto.TenantId;
            IsPublished = updateDto.IsPublished;
            IsLatest = updateDto.IsLatest;

            return this;
        }

        internal void AddPermissions(List<AccessList> accessLists)
        {
            if (Permissions == null)
                Permissions = new List<AccessList>();

            Permissions = Permissions.Union(accessLists).ToList();
        }

        internal void RemovePermissions(List<AccessList> accessLists)
        {
            if (Permissions != null)
                Permissions = Permissions.Except(accessLists).ToList();
        }
    }
}