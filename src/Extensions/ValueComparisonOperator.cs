namespace AspNetCoreNuxt.Extensions
{
    /// <summary>
    /// 値の比較演算子を表します。
    /// </summary>
    public enum ValueComparisonOperator
    {
        /// <summary>
        /// 2 つの値が等しいかどうかを表します。
        /// </summary>
        Equals,

        /// <summary>
        /// 2 つの値が異なるかどうかを表します。
        /// </summary>
        NotEquals,

        /// <summary>
        /// 1 つ目の値が 2 つ目の値より大きいかどうかを表します。
        /// </summary>
        GreaterThan,

        /// <summary>
        /// 1 つ目の値が 2 つ目の値以上かどうかを表します。
        /// </summary>
        GreaterThanOrEquals,

        /// <summary>
        /// 1 つ目の値が 2 つ目の値より小さいかどうかを表します。
        /// </summary>
        LessThan,

        /// <summary>
        /// 1 つ目の値が 2 つ目の値以下かどうかを表します。
        /// </summary>
        LessThanOrEquas,
    }
}
