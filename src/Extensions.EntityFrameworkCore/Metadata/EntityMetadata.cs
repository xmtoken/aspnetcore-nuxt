using Microsoft.EntityFrameworkCore.Metadata;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore.Metadata
{
    /// <inheritdoc cref="IEntityMetadata"/>
    public class EntityMetadata : IEntityMetadata
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

        /// <inheritdoc/>
        public IProperty FindProperty(string propertyName)
            => EntityType.FindProperty(propertyName);
    }
}
