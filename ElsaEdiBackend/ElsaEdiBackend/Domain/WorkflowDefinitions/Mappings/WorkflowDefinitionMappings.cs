using ElsaEdiBackend.Domain.WorkflowDefinitions.Dtos;
using Mapster;

namespace ElsaEdiBackend.Domain.WorkflowDefinitions.Mappings
{
    public class WorkflowDefinitionMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<WorkflowDefinition, WorkflowDefinitionDto>();
            config.NewConfig<WorkflowDefinition, Elsa.Models.WorkflowDefinition>()
                .TwoWays();

            config.NewConfig<AccessList, AccessListForAddDto>()
                .TwoWays();
        }
    }
}