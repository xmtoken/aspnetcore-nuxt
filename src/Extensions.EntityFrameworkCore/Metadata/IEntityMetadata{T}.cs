using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq.Expressions;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore.Metadata
{
    /// <inheritdoc/>
    /// <typeparam name="T">エンティティの型。</typeparam>
    public interface IEntityMetadata<T> : IEntityMetadata
    {
        /// <summary>
        /// 指定されたプロパティのメタデータを取得します。
        /// </summary>
        /// <typeparam name="TProperty">プロパティの型。</typeparam>
        /// <param name="expression">プロパティを示す式ツリー。</param>
        /// <returns><see cref="IProperty"/> オブジェクト。</returns>
        IProperty FindProperty<TProperty>(Expression<Func<T, TProperty>> expression);
    }
}
