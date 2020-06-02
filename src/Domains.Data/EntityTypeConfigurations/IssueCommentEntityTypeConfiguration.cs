using AspNetCoreNuxt.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreNuxt.Domains.Data.EntityTypeConfigurations
{
    /// <inheritdoc cref="IEntityTypeConfiguration{TEntity}"/>
    public class IssueCommentEntityTypeConfiguration : IEntityTypeConfiguration<IssueCommentEntity>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<IssueCommentEntity> builder)
        {
            builder.ToTable("IssueComments")
                   .HasKey(x => x.Id);

            // Configure Properties.
            builder.Property(x => x.Id)
                   .IsRequired()
                   .UseIdentityColumn();

            builder.Property(x => x.IssueId)
                   .IsRequired();

            // Configure Relations.
            builder.HasOne<IssueEntity>()
                   .WithMany(x => x.Comments)
                   .HasForeignKey(x => x.IssueId);
        }
    }
}
