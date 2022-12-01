using ElsaEdiBackend.Resources;

namespace ElsaEdiBackend.Domain.WorkflowDefinitions
{
    public class AccessList : ValueObject
    {
        public string UserName { get; set; }
        public string UserId { get; set; }

        public AccessList()
        {
        }
    }
}