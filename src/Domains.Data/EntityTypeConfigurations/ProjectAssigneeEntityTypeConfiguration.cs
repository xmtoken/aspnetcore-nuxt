using AspNetCoreNuxt.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreNuxt.Domains.Data.EntityTypeConfigurations
{
    /// <inheritdoc cref="IEntityTypeConfiguration{TEntity}"/>
    public class ProjectAssigneeEntityTypeConfiguration : IEntityTypeConfiguration<ProjectAssigneeEntity>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ProjectAssigneeEntity> builder)
        {
            builder.ToTable("ProjectAssignees")
                   .HasKey(x => new { x.ProjectId, x.UserId });

            // Configure Properties.
            builder.Property(x => x.ProjectId)
                   .IsRequired();

            builder.Property(x => x.UserId)
                   .IsRequired();

            // Configure Relations.
            builder.HasOne(x => x.Project)
                   .WithMany(x => x.Assignees)
                   .HasForeignKey(x => x.ProjectId);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.AssignedProjects)
                   .HasForeignKey(x => x.UserId);
        }
    }
}
