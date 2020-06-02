using AspNetCoreNuxt.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreNuxt.Domains.Data.EntityTypeConfigurations
{
    /// <inheritdoc cref="IEntityTypeConfiguration{TEntity}"/>
    public class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRoleEntity>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
        {
            builder.ToTable("UserRoles")
                   .HasKey(x => new { x.UserId, x.RoleId });

            // Configure Properties.
            builder.Property(x => x.UserId)
                   .IsRequired();

            builder.Property(x => x.RoleId)
                   .IsRequired();

            // Configure Relations.
            builder.HasOne<UserEntity>()
                   .WithMany(x => x.Roles)
                   .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Role)
                   .WithMany()
                   .HasForeignKey(x => x.RoleId);
        }
    }
}
