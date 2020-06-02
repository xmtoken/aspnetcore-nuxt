using AspNetCoreNuxt.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreNuxt.Domains.Data.EntityTypeConfigurations
{
    /// <inheritdoc cref="IEntityTypeConfiguration{TEntity}"/>
    public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<ProjectEntity>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ProjectEntity> builder)
        {
            builder.ToTable("Projects")
                   .HasKey(x => x.Id);

            // Configure Properties.
            builder.Property(x => x.Id)
                   .IsRequired()
                   .UseIdentityColumn();

            builder.Property(x => x.Name)
                   .HasMaxLength(256)
                   .IsRequired();
        }
    }
}
