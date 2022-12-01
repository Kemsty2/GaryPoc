using Elsa.Models;
using Elsa.Services;
using Elsa.Services.Models;
using ElsaEdiBackend.Bookmarks;
using ElsaEdiBackend.Dtos;
using Open.Linq.AsyncExtensions;

namespace ElsaEdiBackend.Services
{
    public interface ICreateFileInvoker : IElsaEdiApiService
    {
        Task<IEnumerable<CollectedWorkflow>> DispatchWorkflowsAsync(CreateFileDto file, CancellationToken cancellationToken = default);
        Task<IEnumerable<CollectedWorkflow>> ExecuteWorkflowsAsync(CreateFileDto file, CancellationToken cancellationToken = default);
    }

    public class CreateFileInvoker : ICreateFileInvoker
    {
        private readonly IWorkflowLaunchpad _workflowLaunchpad;

        public CreateFileInvoker(IWorkflowLaunchpad workflowLaunchpad)
        {
            _workflowLaunchpad = workflowLaunchpad;
        }

        public async Task<IEnumerable<CollectedWorkflow>> DispatchWorkflowsAsync(CreateFileDto file, CancellationToken cancellationToken = default)
        {
            var collectedWorkflows = await CollectWorkflowsAsync(file, cancellationToken).ToList();
            await _workflowLaunchpad.DispatchPendingWorkflowsAsync(collectedWorkflows, new WorkflowInput(file), cancellationToken);

            return collectedWorkflows;
        }

        public async Task<IEnumerable<CollectedWorkflow>> ExecuteWorkflowsAsync(CreateFileDto file, CancellationToken cancellationToken = default)
        {
            var collectedWorkflows = await CollectWorkflowsAsync(file, cancellationToken).ToList();
            await _workflowLaunchpad.ExecutePendingWorkflowsAsync(collectedWorkflows, new WorkflowInput(file), cancellationToken);

            return collectedWorkflows;
        }

        private async Task<IEnumerable<CollectedWorkflow>> CollectWorkflowsAsync(CreateFileDto fileModel, CancellationToken cancellationToken)
        {
            var wildcardContext = new WorkflowsQuery(nameof(CreateFileDto), new CreateFileBookmark());

            var wildcardWorkflows = await _workflowLaunchpad.FindWorkflowsAsync(wildcardContext, cancellationToken);

            return wildcardWorkflows;
        }
    }
}
