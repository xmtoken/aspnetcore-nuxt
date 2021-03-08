using System.Collections.Generic;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels
{
    /// <inheritdoc cref="IPagingQuery"/>
    internal class PagingQuery : IPagingQuery
    {
        /// <summary>
        /// <see cref="Limit"/> プロパティへバインディングするモデルの名前を表します。
        /// </summary>
        internal const string LimitBindingModelName = "$limit";

        /// <summary>
        /// <see cref="Offset"/> プロパティへバインディングするモデルの名前を表します。
        /// </summary>
        internal const string OffsetBindingModelName = "$offset";

        /// <inheritdoc/>
        public int? Offset { get; }

        /// <summary>
        /// クエリストリングで指定された値を取得します。
        /// </summary>
        public IReadOnlyCollection<string> OffsetPrimitiveValues { get; }

        /// <inheritdoc/>
        public int? Limit { get; }

        /// <summary>
        /// クエリストリングで指定された値を取得します。
        /// </summary>
        public IReadOnlyCollection<string> LimitPrimitiveValues { get; }

        /// <summary>
        /// <see cref="PagingQuery"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="offset">データの開始位置。</param>
        /// <param name="offsetPrimitiveValues">クエリストリングで指定された値。</param>
        /// <param name="limit">データの上限数。</param>
        /// <param name="limitPrimitiveValues">クエリストリングで指定された値。</param>
        public PagingQuery(int? offset, string[] offsetPrimitiveValues, int? limit, string[] limitPrimitiveValues)
        {
            Offset = offset;
            OffsetPrimitiveValues = offsetPrimitiveValues;
            Limit = limit;
            LimitPrimitiveValues = limitPrimitiveValues;
        }
    }
}
