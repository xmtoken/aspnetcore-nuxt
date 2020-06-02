using AspNetCoreNuxt.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreNuxt.Domains.Data.EntityTypeConfigurations
{
    /// <inheritdoc cref="IEntityTypeConfiguration{TEntity}"/>
    public class UserProfileEntityTypeConfiguration : IEntityTypeConfiguration<UserProfileEntity>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<UserProfileEntity> builder)
        {
            builder.ToTable("UserProfiles")
                   .HasKey(x => x.Id);

            // Configure Indexes.
            builder.HasIndex(x => x.NormalizedEmailAddress);

            // Configure Properties.
            builder.Property(x => x.Id)
                   .IsRequired()
                   .UseIdentityColumn();

            builder.Property(x => x.UserId)
                   .IsRequired();

            builder.Property(x => x.EmailAddress)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(x => x.NormalizedEmailAddress)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(x => x.Birthday)
                   .IsRequired();

            builder.Property(x => x.Gender)
                   .IsRequired();

            // Configure Relations.
            builder.HasOne<UserEntity>()
                   .WithOne(x => x.Profile)
                   .HasForeignKey<UserProfileEntity>(x => x.UserId);
        }
    }
}
