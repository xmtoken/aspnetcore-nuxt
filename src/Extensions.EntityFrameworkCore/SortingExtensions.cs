using AspNetCoreNuxt.Extensions.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="defaults">既定のソート条件とする並び替えのキーとなるプロパティを示す式ツリーと並び替え方向のタプルのコレクション。</param>
        /// <returns><see cref="IQueryableSorting{T}"/> オブジェクト。</returns>
        public static IEnumerable<IQueryableSorting<T>> ToQueryableOrDefault<T>(this IEnumerable<ISorting> source, params (Expression<Func<T, object>> Expression, SortDirection Direction)[] defaults)
        {
            var sortings = source.Select(x => new QueryableSorting<T>(x.SortPropertyName, x.SortDirection)).ToArray();
            return sortings.All(x => x.IsValid()) ? sortings : defaults.Select(x => new QueryableSorting<T>(x.Expression, x.Direction)).ToArray();
        }
    }
}
