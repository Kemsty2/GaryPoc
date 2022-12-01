using Elsa.Events;
using ElsaEdiBackend.Domain.WorkflowDefinitions.Services;
using ElsaEdiBackend.Services;
using MediatR;

namespace ElsaEdiBackend.Domain.WorkflowDefinitions.Features.NotificationHandlers
{
    public class WorkflowPublishPublishedHandler : INotificationHandler<WorkflowDefinitionPublished>
    {
        private readonly IWorkflowDefinitionRepository _workflowDefinitionRepository;
        private readonly ILogger<WorkflowPublishPublishedHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public WorkflowPublishPublishedHandler(IUnitOfWork unitOfWork, ILogger<WorkflowPublishPublishedHandler> logger, IWorkflowDefinitionRepository workflowDefinitionRepository)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _workflowDefinitionRepository = workflowDefinitionRepository;
        }

        public async Task Handle(WorkflowDefinitionPublished notification, CancellationToken cancellationToken)
        {
            try
            {
                var workflowDefinition = await _workflowDefinitionRepository.GetByExpression(x => x.DefinitionId == notification.WorkflowDefinition.DefinitionId, true, cancellationToken);
                if (workflowDefinition != null)
                {
                    workflowDefinition.UpdatePublishedStatus(notification.WorkflowDefinition.IsPublished);
                    _workflowDefinitionRepository.Update(workflowDefinition);
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