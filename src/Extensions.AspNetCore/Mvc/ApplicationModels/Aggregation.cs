namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels
{
    /// <inheritdoc cref="IAggregation"/>
    internal class Aggregation : IAggregation
    {
        /// <inheritdoc/>
        public string PropertyName { get; }

        /// <inheritdoc/>
        public AggregateOperator AggregateOperator { get; }

        /// <summary>
        /// <see cref="Aggregation"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="propertyName">プロパティ名。</param>
        /// <param name="aggregateOperator">集計演算子を表す <see cref="Mvc.AggregateOperator"/> 値。</param>
        public Aggregation(string propertyName, AggregateOperator aggregateOperator)
        {
            PropertyName = propertyName;
            AggregateOperator = aggregateOperator;
        }
    }
}
