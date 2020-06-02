using AspNetCoreNuxt.Extensions.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq.Expressions;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore.Metadata
{
    /// <inheritdoc/>
    /// <typeparam name="T">エンティティの型。</typeparam>
    public class EntityMetadata<T> : EntityMetadata
    {
        /// <summary>
        /// <see cref="EntityMetadata{T}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="entityType"><see cref="IEntityType"/> オブジェクト。</param>
        public EntityMetadata(IEntityType entityType)
            : base(entityType)
        {
        }

        /// <summary>
        /// 指定されたプロパティのメタデータを取得します。
        /// </summary>
        /// <param name="expression">プロパティを示す式ツリー。</param>
        /// <returns><see cref="IProperty"/> オブジェクト。</returns>
        public IProperty Property(Expression<Func<T, object>> expression)
        {
            var name = expression.Body<MemberExpression>().Member.Name;
            return Property(name);
        }
    }
}
