namespace AspNetCoreNuxt.Extensions
{
    /// <summary>
    /// 文字列の比較演算子を表します。
    /// </summary>
    public enum StringComparisonOperator
    {
        /// <summary>
        /// 2 つの文字列が等しいかどうかを表します。
        /// </summary>
        Equals,

        /// <summary>
        /// 2 つの文字列が異なるかどうかを表します。
        /// </summary>
        NotEquals,

        /// <summary>
        /// 1 つ目の文字列が並び替え順序において 2 つ目の文字列より後に位置するどうかを表します。
        /// </summary>
        GreaterThan,

        /// <summary>
        /// 1 つ目の文字列が並び替え順序において 2 つ目の文字列以降に位置するかどうかを表します。
        /// </summary>
        GreaterThanOrEquals,

        /// <summary>
        /// 1 つ目の文字列が並び替え順序において 2 つ目の文字列より前に位置するどうかを表します。
        /// </summary>
        LessThan,

        /// <summary>
        /// 1 つ目の文字列が並び替え順序において 2 つ目の文字列以前に位置するかどうかを表します。
        /// </summary>
        LessThanOrEquas,

        /// <summary>
        /// 1 つ目の文字列が 2 つ目の文字列を含んでいるかどうかを表します。
        /// </summary>
        Contains,

        /// <summary>
        /// 1 つ目の文字列の先頭が 2 つ目の文字列と一致するかどうかを表します。
        /// </summary>
        StartsWith,

        /// <summary>
        /// 1 つ目の文字列の末尾が 2 つ目の文字列と一致するかどうかを表します。
        /// </summary>
        EndsWith,
    }
}
