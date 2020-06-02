using System;
using System.Collections.Generic;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore.Metadata
{
    /// <summary>
    /// エンティティのメタデータのプロバイダーを表します。
    /// </summary>
    public interface IEntityMetadataProvider
    {
        /// <summary>
        /// すべてのエンティティのメタデータを取得します。
        /// </summary>
        /// <returns><see cref="EntityMetadata"/> オブジェクトのコレクション。</returns>
        IEnumerable<EntityMetadata> Entities();

        /// <summary>
        /// 指定された型のエンティティのメタデータを取得します。
        /// </summary>
        /// <param name="type">エンティティの型。</param>
        /// <returns><see cref="EntityMetadata"/> オブジェクト。</returns>
        EntityMetadata Entity(Type type);

        /// <summary>
        /// 指定された型のエンティティのメタデータを取得します。
        /// </summary>
        /// <typeparam name="T">エンティティの型。</typeparam>
        /// <returns><see cref="EntityMetadata{T}"/> オブジェクト。</returns>
        EntityMetadata<T> Entity<T>();
    }
}
