using Microsoft.EntityFrameworkCore.Metadata;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore.Metadata
{
    /// <summary>
    /// エンティティのメタデータを表します。
    /// </summary>
    public interface IEntityMetadata
    {
        /// <summary>
        /// 指定されたプロパティのメタデータを取得します。
        /// </summary>
        /// <param name="propertyName">プロパティ名。</param>
        /// <returns><see cref="IProperty"/> オブジェクト。</returns>
        IProperty FindProperty(string propertyName);
    }
}
