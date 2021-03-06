using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore.Metadata
{
    /// <inheritdoc cref="IEntityMetadataProvider{TContext}"/>
    public class EntityMetadataProvider<TContext> : IEntityMetadataProvider<TContext>
        where TContext : DbContext
    {
        /// <summary>
        /// <see cref="IModel"/> オブジェクトを表します。
        /// </summary>
        private readonly IModel Model;

        /// <summary>
        /// <see cref="EntityMetadataProvider{TContext}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="context"><typeparamref name="TContext"/> オブジェクト。</param>
        public EntityMetadataProvider(TContext context)
        {
            Model = context.Model;
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityMetadata> Entities()
        {
            foreach (var entityType in Model.GetEntityTypes())
            {
                yield return new EntityMetadata(entityType);
            }
        }

        /// <inheritdoc/>
        public IEntityMetadata Entity(Type type)
        {
            var entityType = Model.FindEntityType(type);
            return new EntityMetadata(entityType);
        }

        /// <inheritdoc/>
        public IEntityMetadata<T> Entity<T>()
        {
            var entityType = Model.FindEntityType(typeof(T));
            return new EntityMetadata<T>(entityType);
        }
    }
}
