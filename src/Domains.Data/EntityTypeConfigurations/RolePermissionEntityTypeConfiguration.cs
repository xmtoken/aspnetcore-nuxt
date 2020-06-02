using AspNetCoreNuxt.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreNuxt.Domains.Data.EntityTypeConfigurations
{
    /// <inheritdoc cref="IEntityTypeConfiguration{TEntity}"/>
    public class RolePermissionEntityTypeConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
        {
            builder.ToTable("RolePermissions")
                   .HasKey(x => new { x.RoleId, x.PermissionId });

            // Configure Properties.
            builder.Property(x => x.RoleId)
                   .IsRequired();

            builder.Property(x => x.PermissionId)
                   .IsRequired();

            // Configure Relations.
            builder.HasOne<RoleEntity>()
                   .WithMany(x => x.Permissions)
                   .HasForeignKey(x => x.RoleId);
        }
    }
}
