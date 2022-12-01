using ElsaEdiBackend.Domain.WorkflowInstances;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElsaEdiBackend.Databases.EntityConfigurations
{
    public class WorkflowInstanceConfiguration : IEntityTypeConfiguration<WorkflowInstance>
    {
        public void Configure(EntityTypeBuilder<WorkflowInstance> builder)
        {
            throw new NotImplementedException();
        }
    }
}
