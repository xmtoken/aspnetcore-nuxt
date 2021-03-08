using AspNetCoreNuxt.Domains.Entities._Examples;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Domains.Data.EntityTypeConfigurations._Examples
{
    /// <inheritdoc cref="IEntityTypeConfiguration{TEntity}"/>
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.HasOne(x => x.One)
                   .WithOne()
                   .HasForeignKey<OneEntity>(x => x.CustomerId);

            builder.HasMany(x => x.Many)
                   .WithOne()
                   .HasForeignKey(x => x.CustomerId);

            //builder.ToTable("IssueAssignees")
            //       .HasKey(x => new { x.IssueId, x.UserId });

            //// Configure Properties.
            //builder.Property(x => x.IssueId)
            //       .IsRequired();

            //builder.Property(x => x.UserId)
            //       .IsRequired();

            //// Configure Relations.
            //builder.HasOne(x => x.Issue)
            //       .WithMany(x => x.Assignees)
            //       .HasForeignKey(x => x.IssueId);

            //builder.HasOne(x => x.User)
            //       .WithMany(x => x.AssignedIssues)
            //       .HasForeignKey(x => x.UserId);
        }
    }

    /// <inheritdoc cref="IEntityTypeConfiguration{TEntity}"/>
    public class OneEntityTypeConfiguration : IEntityTypeConfiguration<OneEntity>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<OneEntity> builder)
        {
        }
    }

    /// <inheritdoc cref="IEntityTypeConfiguration{TEntity}"/>
    public class ManyEntityTypeConfiguration : IEntityTypeConfiguration<ManyEntity>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ManyEntity> builder)
        {
        }
    }
}
