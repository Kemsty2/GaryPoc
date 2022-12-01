using ElsaEdiBackend.Domain.WorkflowDefinitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElsaEdiBackend.Databases.EntityConfigurations
{
    public class WorkflowDefinitionConfiguration : IEntityTypeConfiguration<WorkflowDefinition>
    {
        public void Configure(EntityTypeBuilder<WorkflowDefinition> builder)
        {
            builder
                .Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(x => x.DefinitionId)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Tag)
                .HasMaxLength(255)
                .IsRequired(false);

            builder
                .Property(x => x.TenantId)
                .HasMaxLength(255)
                .IsRequired(false);

            builder
                .Property(x => x.Description)
                .HasMaxLength(800)
                .IsRequired(false);
            

            builder.OwnsMany(x => x.Permissions, opts =>
            {
                opts.HasKey("Id");
                opts.WithOwner().HasForeignKey("WorkflowDefinitionId");
                opts.Property<int>("Id");

                opts.Property(x => x.UserName).HasColumnName("permission_username");
                opts.Property(x => x.UserId).HasColumnName("permission_userid");
            })
                .Navigation(x => x.Permissions);

            // Create Index
            builder.HasIndex(x => x.Tag);
            builder.HasIndex(x => x.DefinitionId);
            builder.HasIndex(x => x.TenantId);
            builder.HasIndex(x => x.CreatedBy);
            builder.HasIndex(x => x.LastModifiedBy);
            
        }
    }
}