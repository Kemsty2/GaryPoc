using ElsaEdiBackend.Databases;
using ElsaEdiBackend.Services;

namespace ElsaEdiBackend.Domain.WorkflowInstances.Services
{
    public interface IWorkflowInstanceRepository : IGenericRepository<WorkflowInstance>
    {
    }

    public class WorkflowInstanceRepository : GenericRepository<WorkflowInstance>, IWorkflowInstanceRepository
    {
        private readonly AppDbContext _dbContext;

        public WorkflowInstanceRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
