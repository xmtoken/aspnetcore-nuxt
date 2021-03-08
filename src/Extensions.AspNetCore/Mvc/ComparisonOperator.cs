namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc
{
    /// <summary>
    /// 比較演算子を表します。
    /// </summary>
    public enum ComparisonOperator
    {
        /// <summary>
        /// 2 つの値が等しいことを表します。
        /// </summary>
        Equals,

        /// <summary>
        /// 2 つの値が異なることを表します。
        /// </summary>
        NotEquals,

        /// <summary>
        /// 1 つ目の値が 2 つ目の値より大きいことを表します。
        /// </summary>
        GreaterThan,

        /// <summary>
        /// 1 つ目の値が 2 つ目の値以上であることを表します。
        /// </summary>
        GreaterThanOrEquals,

        /// <summary>
        /// 1 つ目の値が 2 つ目の値より小さいことを表します。
        /// </summary>
        LessThan,

        /// <summary>
        /// 1 つ目の値が 2 つ目の値以下であることを表します。
        /// </summary>
        LessThanOrEquals,

        /// <summary>
        /// 1 つ目の値が 2 つ目の値を含んでいることを表します。
        /// </summary>
        Contains,

        /// <summary>
        /// 1 つ目の値が 2 つ目の値を含んでいないことを表します。
        /// </summary>
        NotContains,

        /// <summary>
        /// 1 つ目の値の先頭が 2 つ目の値と等しいことを表します。
        /// </summary>
        StartsWith,

        /// <summary>
        /// 1 つ目の値の先頭が 2 つ目の値と異なることを表します。
        /// </summary>
        NotStartsWith,

        /// <summary>
        /// 1 つ目の値の末尾が 2 つ目の値と等しいことを表します。
        /// </summary>
        EndsWith,

        /// <summary>
        /// 1 つ目の値の末尾が 2 つ目の値と異なることを表します。
        /// </summary>
        NotEndsWith,

        /// <summary>
        /// 値が null であることを表します。
        /// </summary>
        IsNull,
    }
}
