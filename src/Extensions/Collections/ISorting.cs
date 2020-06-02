namespace AspNetCoreNuxt.Extensions.Collections
{
    /// <summary>
    /// ソート条件を表します。
    /// </summary>
    public interface ISorting
    {
        /// <summary>
        /// 並び替え方向を取得します。
        /// </summary>
        SortDirection SortDirection { get; }

        /// <summary>
        /// 並び替えのキーとなるプロパティの名前を取得します。
        /// </summary>
        string SortPropertyName { get; }
    }
}
