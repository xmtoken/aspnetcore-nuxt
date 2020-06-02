using System.Collections.Generic;

namespace AspNetCoreNuxt.Extensions.Collections.Generic
{
    /// <summary>
    /// ページングされたコレクション情報を表します。
    /// </summary>
    /// <typeparam name="T">コレクションの要素の型。</typeparam>
    public interface IPagination<T>
    {
        /// <summary>
        /// 1 から始まる現在のページインデックスを取得します。
        /// </summary>
        int CurrentPageIndex { get; }

        /// <summary>
        /// 現在のページの要素のコレクションを取得します。
        /// </summary>
        IEnumerable<T> CurrentPageItems { get; }

        /// <summary>
        /// 現在のページングサイズを取得します。
        /// </summary>
        int CurrentPageSize { get; }

        /// <summary>
        /// 要素数を取得します。
        /// </summary>
        int TotalItemCount { get; }

        /// <summary>
        /// ページ数を取得します。
        /// </summary>
        int TotalPageCount { get; }
    }
}
