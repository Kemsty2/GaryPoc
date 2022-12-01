using Elsa.Models;
using Elsa.Persistence.EntityFramework.Core;
using Microsoft.EntityFrameworkCore;

namespace ElsaEdiBackend.Databases
{
    public class ElsaCustomContext : ElsaContext
    {
        public ElsaCustomContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WorkflowDefinition>()
                .ToTable("wf_definition");

            modelBuilder.Entity<WorkflowInstance>()
                .ToTable("wf_instance");

            modelBuilder.Entity<Bookmark>()
                .ToTable("bm");
        }
    }
}