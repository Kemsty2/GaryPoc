using Elsa.Events;
using ElsaEdiBackend.Domain.WorkflowInstances.Services;
using ElsaEdiBackend.Services;
using MediatR;

namespace ElsaEdiBackend.Domain.WorkflowDefinitions.Features.NotificationHandlers
{
    public class WorkflowExecutionFinishedHandler : INotificationHandler<WorkflowExecutionFinished>
    {
        private readonly IWorkflowInstanceRepository _workflowInstanceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<WorkflowExecutionFinishedHandler> _logger;

        public WorkflowExecutionFinishedHandler(IWorkflowInstanceRepository workflowInstanceRepository, IUnitOfWork unitOfWork, ILogger<WorkflowExecutionFinishedHandler> logger)
        {
            _workflowInstanceRepository = workflowInstanceRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(WorkflowExecutionFinished notification, CancellationToken cancellationToken)
        {
            try
            {
                var instance = await _workflowInstanceRepository.GetByExpression(x => x.InstanceId == notification.WorkflowExecutionContext.WorkflowInstance.Id, true, cancellationToken);
                if (instance == null)
                    return;
                instance.UpdateStatus(notification.WorkflowExecutionContext.Status);
                _workflowInstanceRepository.Update(instance);
                await _unitOfWork.CommitChanges(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An unknown error have occurred when updating workflow instance {id}", notification.WorkflowExecutionContext.WorkflowInstance.Id);
            }
        }
    }
}