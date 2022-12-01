using Elsa.Events;
using ElsaEdiBackend.Domain.WorkflowDefinitions.Services;
using ElsaEdiBackend.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ElsaEdiBackend.Domain.WorkflowDefinitions.Features.NotificationHandlers
{
    public class WorkflowsDeletedHandler : INotificationHandler<ManyWorkflowDefinitionsDeleted>
    {
        private readonly IWorkflowDefinitionRepository _workflowDefinitionRepository;
        private readonly ILogger<WorkflowsDeletedHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public WorkflowsDeletedHandler(IUnitOfWork unitOfWork, ILogger<WorkflowsDeletedHandler> logger, IWorkflowDefinitionRepository workflowDefinitionRepository)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _workflowDefinitionRepository = workflowDefinitionRepository;
        }

        public async Task Handle(ManyWorkflowDefinitionsDeleted notification, CancellationToken cancellationToken)
        {
            // Delete Workflows Definition in LocalDatabase
            try
            {
                var definitionIds = notification.WorkflowDefinitions.Select(w => w.DefinitionId).ToList();

                var workflowsDefinition = await _workflowDefinitionRepository
                    .Query()
                    .Where(x => definitionIds.Contains(x.DefinitionId))
                    .ToListAsync(cancellationToken: cancellationToken);

                _workflowDefinitionRepository.RemoveRange(workflowsDefinition);
                await _unitOfWork.CommitChanges(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred when saving entity in local database");
            }
        }
    }
}