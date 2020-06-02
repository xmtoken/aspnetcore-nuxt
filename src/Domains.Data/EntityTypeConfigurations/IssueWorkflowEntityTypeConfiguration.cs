using AspNetCoreNuxt.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreNuxt.Domains.Data.EntityTypeConfigurations
{
    /// <inheritdoc cref="IEntityTypeConfiguration{TEntity}"/>
    public class IssueWorkflowEntityTypeConfiguration : IEntityTypeConfiguration<IssueWorkflowEntity>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<IssueWorkflowEntity> builder)
        {
            builder.ToTable("IssueWorkflows")
                   .HasKey(x => x.Id);

            // Configure Properties.
            builder.Property(x => x.Id)
                   .IsRequired()
                   .UseIdentityColumn();

            builder.Property(x => x.Name)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(x => x.Color)
                   .HasMaxLength(9)
                   .IsRequired();
        }
    }
}
