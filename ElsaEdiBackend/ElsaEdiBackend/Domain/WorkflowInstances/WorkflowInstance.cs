using ElsaEdiBackend.Domain.WorkflowDefinitions;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ElsaEdiBackend.Domain.WorkflowInstances
{
    public class WorkflowInstance : BaseEntity
    {
        public string? InstanceId { get; set; }
        public string? Name { get; set; }        
        public string? CorrelationId { get; set; }
        public Elsa.Models.WorkflowStatus WorkflowStatus { get; set; }
        public int Version { get; set; }
        public string? TenantId { get; set;  }
        public string? ActivityId { get; set; }        
        
        [JsonIgnore]
        [IgnoreDataMember]
        public string? WorkflowDefinitionId { get; set; }

        public virtual WorkflowDefinition? WorkflowDefinition { get; set; }

        public WorkflowInstance()
        { }

        public WorkflowInstance(string id)
        {
            WorkflowDefinitionId = id;
        }

        public void SetWorkflowInstance(Elsa.Models.WorkflowInstance? instance, string? activityId)
        {
            if(instance != null)
            {
                InstanceId = instance.Id;
                Name = instance.Name;
                CorrelationId = instance.CorrelationId;
                WorkflowStatus = instance.WorkflowStatus;
                Version = instance.Version;
                TenantId = instance.TenantId;
                ActivityId = activityId;
            }
            
        }

        public void UpdateStatus(Elsa.Models.WorkflowStatus status)
        {
            WorkflowStatus = status;
        }
    }
}