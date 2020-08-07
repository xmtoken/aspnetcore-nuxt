using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq.Expressions;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore.Metadata
{
    /// <inheritdoc cref="IEntityMetadata{T}"/>
    public class EntityMetadata<T> : EntityMetadata, IEntityMetadata<T>
    {
        /// <summary>
        /// <see cref="EntityMetadata{T}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="entityType"><see cref="IEntityType"/> オブジェクト。</param>
        public EntityMetadata(IEntityType entityType)
            : base(entityType)
        {
        }

        /// <inheritdoc/>
        public IProperty FindProperty<TProperty>(Expression<Func<T, TProperty>> expression)
            => FindProperty((LambdaExpression)expression);
    }
}
