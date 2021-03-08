using System;
using System.Collections.Generic;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels
{
    /// <summary>
    /// 検索条件を表します。
    /// </summary>
    /// <typeparam name="T">値の型。</typeparam>
    public class SearchConditions<T>
    {
        /// <summary>
        /// 比較演算子と比較する値のコレクションを取得します。
        /// </summary>
        internal IReadOnlyCollection<SearchConditionsComparison<T>> Comparisons { get; }

        /// <summary>
        /// 論理演算子を表す <see cref="Mvc.LogicalOperator"/> 値を取得します。
        /// </summary>
        internal LogicalOperator LogicalOperator { get; }

        /// <summary>
        /// クエリストリングで指定された値を取得します。
        /// </summary>
        internal IReadOnlyCollection<string> PrimitiveValues { get; }

        /// <summary>
        /// モデルバインディング先のプロパティ名を取得します。
        /// </summary>
        internal string PropertyName { get; }

        /// <summary>
        /// <see cref="SearchConditions{T}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="primitiveValues">クエリストリングで指定された値。</param>
        /// <param name="propertyName">モデルバインディング先のプロパティ名。</param>
        /// <param name="logicalOperator">論理演算子を表す <see cref="Mvc.LogicalOperator"/> 値。</param>
        /// <param name="comparisons">比較演算子と比較する値のコレクション。</param>
        public SearchConditions(string[] primitiveValues, string propertyName, LogicalOperator logicalOperator, SearchConditionsComparison<T>[] comparisons)
        {
            PrimitiveValues = Array.AsReadOnly(primitiveValues);
            PropertyName = propertyName;
            LogicalOperator = logicalOperator;
            Comparisons = Array.AsReadOnly(comparisons);
        }
    }
}
