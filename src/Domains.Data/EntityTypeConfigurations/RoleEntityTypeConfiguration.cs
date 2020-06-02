using AspNetCoreNuxt.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreNuxt.Domains.Data.EntityTypeConfigurations
{
    /// <inheritdoc cref="IEntityTypeConfiguration{TEntity}"/>
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("Roles")
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
