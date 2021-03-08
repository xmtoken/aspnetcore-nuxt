namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels
{
    /// <summary>
    /// 検索条件の比較演算子と比較値を表します。
    /// </summary>
    /// <typeparam name="T">値の型。</typeparam>
    public class SearchConditionsComparison<T>
    {
        /// <summary>
        /// 比較演算子を表す <see cref="Mvc.ComparisonOperator"/>  値を取得します。
        /// </summary>
        public ComparisonOperator ComparisonOperator { get; }

        /// <summary>
        /// 比較する値を取得します。
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// <see cref="SearchConditionsComparison{T}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="comparisonOperator">比較演算子を表す <see cref="Mvc.ComparisonOperator"/>  値。</param>
        /// <param name="value">比較する値。</param>
        internal SearchConditionsComparison(ComparisonOperator comparisonOperator, T value)
        {
            ComparisonOperator = comparisonOperator;
            Value = value;
        }
    }
}
