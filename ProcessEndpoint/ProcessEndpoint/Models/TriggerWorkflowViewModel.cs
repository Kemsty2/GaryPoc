namespace ProcessEndpoint.Models
{
    public class TriggerWorkflowViewModel
    {
        public string WorkflowId { get; set; }
        public string? Payload { get; set; }

        public TriggerWorkflowViewModel()
        {
            WorkflowId = "";
        }
    }
}
