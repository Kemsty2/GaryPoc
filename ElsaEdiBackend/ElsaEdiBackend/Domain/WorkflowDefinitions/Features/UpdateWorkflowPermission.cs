using AutoWrapper.Wrappers;
using ElsaEdiBackend.Domain.WorkflowDefinitions.Dtos;
using ElsaEdiBackend.Domain.WorkflowDefinitions.Services;
using ElsaEdiBackend.Services;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ElsaEdiBackend.Domain.WorkflowDefinitions.Features
{
    public static class UpdateWorkflowPermission
    {
        public sealed class Command : IRequest<bool>
        {
            public readonly string Id;
            public readonly AccessListForPatchDto Payload;
            public bool ToAdd { get; set; }

            public Command(string id, AccessListForPatchDto payload, bool toAdd = true)
            {
                Id = id;
                Payload = payload;
                ToAdd = toAdd;
            }
        }

        public sealed class Handler : IRequestHandler<Command, bool>
        {
            private readonly IWorkflowDefinitionRepository _workflowDefinitionRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IWorkflowDefinitionRepository workflowDefinitionRepository, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _workflowDefinitionRepository = workflowDefinitionRepository;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var workflow = await _workflowDefinitionRepository.GetById(request.Id, true, cancellationToken);                    

                if (workflow == null)
                    throw new ApiException($"Workflow with Id {request.Id} is not found or deleted", 404);
                if (request.Payload.Permissions != null)
                {
                    if (request.ToAdd)
                    {
                        workflow.AddPermissions(_mapper.Map<List<AccessList>>(request.Payload.Permissions));
                    }
                    else
                    {
                        workflow.RemovePermissions(_mapper.Map<List<AccessList>>(request.Payload.Permissions));
                    }
                }                    
                _workflowDefinitionRepository.Update(workflow);

                return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
            }
        }
    }
}