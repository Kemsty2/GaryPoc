using ElsaEdiBackend.Databases;
using ElsaEdiBackend.Services;

namespace ElsaEdiBackend.Domain.WorkflowDefinitions.Services;

public interface IWorkflowDefinitionRepository : IGenericRepository<WorkflowDefinition>
{    
}

public class WorkflowDefinitionRepository : GenericRepository<WorkflowDefinition>, IWorkflowDefinitionRepository
{
    private readonly AppDbContext _dbContext;

    public WorkflowDefinitionRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }    
}