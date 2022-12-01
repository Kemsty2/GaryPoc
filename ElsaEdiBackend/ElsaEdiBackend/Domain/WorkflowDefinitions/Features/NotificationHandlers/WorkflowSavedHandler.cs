using Elsa.Events;
using ElsaEdiBackend.Domain.WorkflowDefinitions.Services;
using ElsaEdiBackend.Services;
using MapsterMapper;
using MediatR;

namespace ElsaEdiBackend.Domain.WorkflowDefinitions.Features.NotificationHandlers
{
    public class WorkflowSavedHandler : INotificationHandler<WorkflowDefinitionSaved>
    {
        private readonly IWorkflowDefinitionRepository _workflowDefinitionRepository;
        private readonly ILogger<WorkflowSavedHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public WorkflowSavedHandler(IWorkflowDefinitionRepository workflowDefinitionRepository, ILogger<WorkflowSavedHandler> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _workflowDefinitionRepository = workflowDefinitionRepository;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(WorkflowDefinitionSaved notification, CancellationToken cancellationToken)
        {
            try
            {
                //  Create or Update Workflow Definition in LocalDatabase
                var workflowDefinition = await _workflowDefinitionRepository.GetByExpression(x => x.DefinitionId == notification.WorkflowDefinition.DefinitionId, true, cancellationToken);
                if (workflowDefinition == null)
                {
                    // Create Workflow
                    await _workflowDefinitionRepository.Add(_mapper.Map<WorkflowDefinition>(notification.WorkflowDefinition), cancellationToken);
                }
                else
                {
                    // Update Worklfow
                    _workflowDefinitionRepository.Update(workflowDefinition.Update(notification.WorkflowDefinition));
                }
                await _unitOfWork.CommitChanges(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred when saving entity in local database");
            }
        }
    }
}