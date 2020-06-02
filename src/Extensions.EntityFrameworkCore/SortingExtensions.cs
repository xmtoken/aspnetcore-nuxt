using AspNetCoreNuxt.Extensions.Collections;
using System;
using System.Linq.Expressions;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore
{
    /// <summary>
    /// <see cref="ISorting"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class SortingExtensions
    {
        /// <summary>
        /// <see cref="IQueryableSorting{T}"/> オブジェクトへ変換します。
        /// 変換できない場合には指定された情報をもとに新しい <see cref="IQueryableSorting{T}"/> オブジェクトを返します。
        /// </summary>
        /// <typeparam name="T">並び替えのキーとなるプロパティのトップレベルのオブジェクトの型。</typeparam>
        /// <param name="source"><see cref="ISorting"/> オブジェクト。</param>
        /// <param name="defaultExpression">並び替えのキーとなるプロパティを示す式ツリー。</param>
        /// <param name="defaultDirection">並び替え方向。</param>
        /// <returns><see cref="IQueryableSorting{T}"/> オブジェクト。</returns>
        public static IQueryableSorting<T> ToQueryableOrDefault<T>(this ISorting source, Expression<Func<T, object>> defaultExpression, SortDirection defaultDirection)
        {
            var sorting = new QueryableSorting<T>(source.SortPropertyName, source.SortDirection);
            return sorting.IsValid() ? sorting : new QueryableSorting<T>(defaultExpression, defaultDirection);
        }
    }
}
