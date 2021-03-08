using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels
{
    /// <inheritdoc cref="IAggregateQuery{T}"/>
    internal class AggregateQuery<T> : IAggregateQuery<T>
    {
        /// <summary>
        /// バインディングするモデルの名前を表します。
        /// </summary>
        internal const string BindingModelName = "$aggregate";

        /// <summary>
        /// <see cref="AggregateQuery{T}"/> クラスの既定のインスタンスを取得します。
        /// </summary>
        internal static AggregateQuery<T> Default { get; } = new AggregateQuery<T>(Array.Empty<IAggregation>(), Array.Empty<string>());

        /// <inheritdoc/>
        IEnumerable<string> IAggregateQuery<T>.PropertyNames => Aggregations.Select(x => x.PropertyName);

        /// <summary>
        /// <see cref="IAggregation"/> オブジェクトのコレクションを取得します。
        /// </summary>
        public IReadOnlyCollection<IAggregation> Aggregations { get; }

        /// <summary>
        /// クエリストリングで指定された値を取得します。
        /// </summary>
        public IReadOnlyCollection<string> PrimitiveValues { get; }

        /// <summary>
        /// <see cref="AggregateQuery{T}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="aggregations"><see cref="IAggregation"/> オブジェクトのコレクション。</param>
        /// <param name="primitiveValues">クエリストリングで指定された値。</param>
        public AggregateQuery(IAggregation[] aggregations, string[] primitiveValues)
        {
            Aggregations = Array.AsReadOnly(aggregations);
            PrimitiveValues = Array.AsReadOnly(primitiveValues);
        }

        /// <inheritdoc/>
        public IEnumerable<IAggregation> GetAggregations() => Aggregations;
    }
}
