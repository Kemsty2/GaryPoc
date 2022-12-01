namespace ProcessEndpoint.Models
{
    public class TriggerWorkflowDto
    {
        public string? Payload { get; set; }

        public TriggerWorkflowDto(string? payload)
        {
            Payload = payload;
        }
    }
}