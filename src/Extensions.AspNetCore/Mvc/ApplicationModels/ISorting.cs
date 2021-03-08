using AspNetCoreNuxt.Extensions.Collections;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels
{
    /// <summary>
    /// ソート情報を表します。
    /// </summary>
    public interface ISorting
    {
        /// <summary>
        /// プロパティ名を取得します。
        /// </summary>
        string PropertyName { get; }

        /// <summary>
        /// ソート方向を表す <see cref="Collections.SortDirection"/> 値を取得します。
        /// </summary>
        SortDirection SortDirection { get; }
    }
}
