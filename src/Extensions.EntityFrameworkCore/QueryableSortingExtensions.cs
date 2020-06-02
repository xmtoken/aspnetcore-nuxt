namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore
{
    /// <summary>
    /// <see cref="IQueryableSorting{T}"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class QueryableSortingExtensions
    {
        /// <summary>
        /// ソート条件が有効かどうかを返します。
        /// </summary>
        /// <typeparam name="T">並び替えのキーとなるプロパティのトップレベルのオブジェクトの型。</typeparam>
        /// <param name="sorting"><see cref="IQueryableSorting{T}"/> オブジェクト。</param>
        /// <returns>ソート条件が有効な場合は true。それ以外の場合は false。</returns>
        public static bool IsValid<T>(this IQueryableSorting<T> sorting)
            => sorting.SortExpression != null;
    }
}
