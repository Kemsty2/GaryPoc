using ElsaEdiBackend.Dtos;

namespace ElsaEdiBackend.Domain.WorkflowDefinitions.Dtos
{
    public class WorkflowDefinitionParametersDto : BasePaginationParameters
    {
        public string? Filters { get; set; }
        public string? SortOrder { get; set; }                
    }
}
