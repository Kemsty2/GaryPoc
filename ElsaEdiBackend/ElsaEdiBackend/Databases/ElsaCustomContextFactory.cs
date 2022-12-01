using Elsa.Persistence.EntityFramework.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ElsaEdiBackend.Databases;

public class ElsaCustomContextFactory : IDesignTimeDbContextFactory<ElsaContext>
{
    public ElsaContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddCommandLine(args)
                .Build();

        var builder = new DbContextOptionsBuilder<ElsaCustomContext>();
        var connectionString = "Data Source=elsa.sqlite.db;Cache=Shared;";

        builder.UseSqlite(connectionString, sqlite => sqlite
            .MigrationsAssembly(typeof(ElsaCustomContextFactory).Assembly.FullName)
            .MigrationsHistoryTable(ElsaContext.MigrationsHistoryTable, ElsaContext.ElsaSchema)            
            );

        return new ElsaCustomContext(builder.Options);
    }
}