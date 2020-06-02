using Microsoft.EntityFrameworkCore.Metadata;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore.Metadata
{
    /// <summary>
    /// エンティティのメタデータを表します。
    /// </summary>
    public class EntityMetadata
    {
        /// <summary>
        /// <see cref="IEntityType"/> オブジェクトを表します。
        /// </summary>
        private readonly IEntityType EntityType;

        /// <summary>
        /// <see cref="EntityMetadata"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="entityType"><see cref="IEntityType"/> オブジェクト。</param>
        public EntityMetadata(IEntityType entityType)
        {
            EntityType = entityType;
        }

        /// <summary>
        /// 指定されたプロパティのメタデータを取得します。
        /// </summary>
        /// <param name="propertyName">プロパティ名。</param>
        /// <returns><see cref="IProperty"/> オブジェクト。</returns>
        public IProperty Property(string propertyName)
            => EntityType.FindProperty(propertyName);
    }
}
