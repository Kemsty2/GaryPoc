using ElsaEdiBackend.Domain.WorkflowDefinitions.Dtos;
using ElsaEdiBackend.Domain.WorkflowDefinitions.Services;
using MapsterMapper;
using MediatR;

namespace ElsaEdiBackend.Domain.WorkflowDefinitions.Features
{
    public static class GetByWorkflowDefinitionById
    {
        public sealed class Query : IRequest<WorkflowDefinitionDto>
        {
            public readonly string Id;

            public Query(string id)
            {
                Id = id;
            }
        }

        public sealed class Handler : IRequestHandler<Query, WorkflowDefinitionDto>
        {
            private readonly IWorkflowDefinitionRepository _workflowDefinitionRepository;
            private readonly IMapper _mapper;

            public Handler(IWorkflowDefinitionRepository workflowDefinitionRepository, IMapper mapper)
            {
                _workflowDefinitionRepository = workflowDefinitionRepository;
                _mapper = mapper;
            }

            public async Task<WorkflowDefinitionDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _workflowDefinitionRepository.GetById(request.Id);
                return _mapper.Map<WorkflowDefinitionDto>(result);
            }
        }
    }
}