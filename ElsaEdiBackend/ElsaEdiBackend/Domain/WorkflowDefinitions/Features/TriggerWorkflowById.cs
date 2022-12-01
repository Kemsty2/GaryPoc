using AutoWrapper.Wrappers;
using Elsa.Services;
using Elsa.Services.Models;
using ElsaEdiBackend.Domain.WorkflowDefinitions.Dtos;
using ElsaEdiBackend.Domain.WorkflowDefinitions.Services;
using ElsaEdiBackend.Domain.WorkflowInstances;
using ElsaEdiBackend.Domain.WorkflowInstances.Services;
using ElsaEdiBackend.Services;
using MediatR;

namespace ElsaEdiBackend.Domain.WorkflowDefinitions.Features
{
    public static class TriggerWorkflowById
    {
        public sealed class Command : IRequest<WorkflowInstance>
        {
            public readonly string Id;
            public readonly TriggerWorkflowDto Payload;

            public Command(string id, TriggerWorkflowDto payload)
            {
                Id = id;
                Payload = payload;
            }
        }

        public sealed class Handler : IRequestHandler<Command, WorkflowInstance>
        {
            private readonly IWorkflowLaunchpad _workflowLaunchpad;
            private readonly IWorkflowDefinitionRepository _workflowDefinitionRepository;
            private readonly IWorkflowInstanceRepository _workflowInstanceRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;

            public Handler(IWorkflowLaunchpad workflowLaunchpad, IWorkflowDefinitionRepository workflowDefinitionRepository, ILogger<Handler> logger, IWorkflowInstanceRepository workflowInstanceRepository, IUnitOfWork unitOfWork)
            {
                _workflowLaunchpad = workflowLaunchpad;
                _workflowDefinitionRepository = workflowDefinitionRepository;
                _logger = logger;
                _workflowInstanceRepository = workflowInstanceRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<WorkflowInstance> Handle(Command request, CancellationToken cancellationToken)
            {
                var workflow = await _workflowDefinitionRepository.GetById(request.Id, false, cancellationToken);
                var startableWorkflow = await _workflowLaunchpad.FindStartableWorkflowAsync(workflow.DefinitionId!, tenantId: workflow.TenantId, cancellationToken: cancellationToken);

                if (startableWorkflow is null)
                {
                    throw new ApiException("Workflow Definition with Id {request.Id} not found or deleted. Please contact administrator", 404);
                }

                var instance = await SaveInDb(request.Id, cancellationToken);
                return await LaunchWorkflow(request, instance, startableWorkflow, workflow.DefinitionId, cancellationToken); ;
            }

            private async Task<WorkflowInstance> SaveInDb(string worflowDefinitionId, CancellationToken cancellationToken)
            {
                try
                {
                    var instance = new WorkflowInstance(worflowDefinitionId);
                    await _workflowInstanceRepository.Add(instance, cancellationToken);
                    await _unitOfWork.CommitChanges(cancellationToken);

                    return instance;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "An unknown error occurred when saving workflow instance of workflow {id}", worflowDefinitionId);
                    throw new ApiException($"An unknown error occurred when saving workflow instance of workflow {worflowDefinitionId}", 500);
                }
            }

            private async Task<WorkflowInstance> LaunchWorkflow(Command request, WorkflowInstance instance, StartableWorkflow startableWorkflow, string? definitionId, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _workflowLaunchpad.ExecuteStartableWorkflowAsync(startableWorkflow, new Elsa.Models.WorkflowInput(request.Payload), cancellationToken);
                    _logger.LogInformation("Workflow {0} have been started with Result {1}", definitionId, result.Executed);

                    //  Create workflow instance
                    instance.SetWorkflowInstance(result.WorkflowInstance, result.ActivityId);
                    return instance;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "An unknown error occurred when launching workflow {id}", request.Id);
                    instance.UpdateStatus(Elsa.Models.WorkflowStatus.Faulted);

                    throw new ApiException($"An unknown error occurred when launching workflow {request.Id}");
                }
                finally
                {
                    await _unitOfWork.CommitChanges(cancellationToken);
                }
            }
        }
    }
}