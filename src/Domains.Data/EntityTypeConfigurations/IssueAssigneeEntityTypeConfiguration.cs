using AspNetCoreNuxt.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreNuxt.Domains.Data.EntityTypeConfigurations
{
    /// <inheritdoc cref="IEntityTypeConfiguration{TEntity}"/>
    public class IssueAssigneeEntityTypeConfiguration : IEntityTypeConfiguration<IssueAssigneeEntity>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<IssueAssigneeEntity> builder)
        {
            builder.ToTable("IssueAssignees")
                   .HasKey(x => new { x.IssueId, x.UserId });

            // Configure Properties.
            builder.Property(x => x.IssueId)
                   .IsRequired();

            builder.Property(x => x.UserId)
                   .IsRequired();

            // Configure Relations.
            builder.HasOne(x => x.Issue)
                   .WithMany(x => x.Assignees)
                   .HasForeignKey(x => x.IssueId);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.AssignedIssues)
                   .HasForeignKey(x => x.UserId);
        }
    }
}
