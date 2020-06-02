using AspNetCoreNuxt.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreNuxt.Domains.Data.EntityTypeConfigurations
{
    /// <inheritdoc cref="IEntityTypeConfiguration{TEntity}"/>
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users")
                   .HasKey(x => x.Id);

            // Configure Indexes.
            builder.HasIndex(x => x.NormalizedName)
                   .IsUnique();

            // Configure Properties.
            builder.Property(x => x.Id)
                   .IsRequired()
                   .UseIdentityColumn();

            builder.Property(x => x.Name)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(x => x.NormalizedName)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(x => x.PasswordHash)
                   .IsRequired();
        }
    }
}
