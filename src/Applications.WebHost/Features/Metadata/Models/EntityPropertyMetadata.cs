namespace AspNetCoreNuxt.Applications.WebHost.Features.Metadata.Models
{
    /// <summary>
    /// エンティティのプロパティのメタデータを表します。
    /// </summary>
    public class EntityPropertyMetadata
    {
        /// <summary>
        /// プロパティの最大文字数を取得または設定します。
        /// </summary>
        public int? MaxLength { get; set; }

        /// <summary>
        /// プロパティが必須かどうかを取得または設定します。
        /// </summary>
        public bool Required { get; set; }
    }
}
