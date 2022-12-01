using Elsa.Events;
using ElsaEdiBackend.Domain.WorkflowDefinitions.Services;
using ElsaEdiBackend.Services;
using MediatR;

namespace ElsaEdiBackend.Domain.WorkflowDefinitions.Features.NotificationHandlers
{
    public class WorkflowDeletedHandler : INotificationHandler<WorkflowDefinitionDeleted>
    {
        private readonly IWorkflowDefinitionRepository _workflowDefinitionRepository;
        private readonly ILogger<WorkflowDeletedHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public WorkflowDeletedHandler(IUnitOfWork unitOfWork, ILogger<WorkflowDeletedHandler> logger, IWorkflowDefinitionRepository workflowDefinitionRepository)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _workflowDefinitionRepository = workflowDefinitionRepository;
        }

        public async Task Handle(WorkflowDefinitionDeleted notification, CancellationToken cancellationToken)
        {
            //Delete Workflow Definition in LocalDatabase
            try
            {
                var workflowDefinition = await _workflowDefinitionRepository.GetByExpression(x => x.DefinitionId == notification.WorkflowDefinition.DefinitionId, true, cancellationToken);
                if (workflowDefinition != null)
                {                    
                    _workflowDefinitionRepository.Remove(workflowDefinition);
                    await _unitOfWork.CommitChanges(cancellationToken);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred when saving entity in local database");
            }
        }
    }
}