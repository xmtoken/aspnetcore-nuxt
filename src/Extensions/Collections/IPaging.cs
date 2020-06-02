namespace AspNetCoreNuxt.Extensions.Collections
{
    /// <summary>
    /// ページング条件を表します。
    /// </summary>
    public interface IPaging
    {
        /// <summary>
        /// 1 から始まるページインデックスを取得します。
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// ページングサイズを取得します。
        /// </summary>
        int PageSize { get; }
    }
}
