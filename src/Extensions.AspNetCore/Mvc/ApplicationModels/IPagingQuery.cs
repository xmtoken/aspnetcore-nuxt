using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels
{
    /// <summary>
    /// ページングのクエリ情報を表します。
    /// </summary>
    public interface IPagingQuery
    {
        /// <summary>
        /// データの上限数を取得します。
        /// </summary>
        [FromQuery(Name = PagingQuery.LimitBindingModelName)]
        int? Limit { get; }

        /// <summary>
        /// データの開始位置を取得します。
        /// </summary>
        [FromQuery(Name = PagingQuery.OffsetBindingModelName)]
        int? Offset { get; }
    }
}
