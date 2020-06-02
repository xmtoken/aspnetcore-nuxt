using System.Collections.Generic;

namespace AspNetCoreNuxt.Extensions.Collections.Generic
{
    /// <inheritdoc cref="IPagination{T}"/>
    public class Pagination<T> : IPagination<T>
    {
        /// <summary>
        /// 1 から始まる現在のページインデックスを取得または設定します。
        /// </summary>
        public int CurrentPageIndex { get; set; }

        /// <summary>
        /// 現在のページの要素のコレクションを取得または設定します。
        /// </summary>
        public IEnumerable<T> CurrentPageItems { get; set; }

        /// <summary>
        /// 現在のページングサイズを取得または設定します。
        /// </summary>
        public int CurrentPageSize { get; set; }

        /// <summary>
        /// 要素数を取得または設定します。
        /// </summary>
        public int TotalItemCount { get; set; }

        /// <summary>
        /// ページ数を取得または設定します。
        /// </summary>
        public int TotalPageCount { get; set; }
    }
}
