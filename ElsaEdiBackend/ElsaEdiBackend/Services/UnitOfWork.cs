namespace ElsaEdiBackend.Services;

using ElsaEdiBackend.Databases;

public interface IUnitOfWork : IElsaEdiApiService
{
    Task<int> CommitChanges(CancellationToken cancellationToken = default);
}

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CommitChanges(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
