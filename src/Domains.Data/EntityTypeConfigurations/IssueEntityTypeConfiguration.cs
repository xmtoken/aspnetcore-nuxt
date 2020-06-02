using AspNetCoreNuxt.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreNuxt.Domains.Data.EntityTypeConfigurations
{
    /// <inheritdoc cref="IEntityTypeConfiguration{TEntity}"/>
    public class IssueEntityTypeConfiguration : IEntityTypeConfiguration<IssueEntity>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<IssueEntity> builder)
        {
            builder.ToTable("Issues")
                   .HasKey(x => x.Id);

            // Configure Properties.
            builder.Property(x => x.Id)
                   .IsRequired()
                   .UseIdentityColumn();

            builder.Property(x => x.Name)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(x => x.ProjectId)
                   .IsRequired();

            builder.Property(x => x.WorkflowId)
                   .IsRequired();

            // Configure Relations.
            builder.HasOne(x => x.Project)
                   .WithMany(x => x.Issues)
                   .HasForeignKey(x => x.ProjectId);

            builder.HasOne(x => x.Workflow)
                   .WithMany()
                   .HasForeignKey(x => x.WorkflowId);
        }
    }
}
