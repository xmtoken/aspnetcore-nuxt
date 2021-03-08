namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels
{
    /// <summary>
    /// 集計情報を表します。
    /// </summary>
    public interface IAggregation
    {
        /// <summary>
        /// プロパティ名を取得します。
        /// </summary>
        string PropertyName { get; }

        /// <summary>
        /// 集計演算子を表す <see cref="Mvc.AggregateOperator"/> 値を取得します。
        /// </summary>
        AggregateOperator AggregateOperator { get; }
    }
}
