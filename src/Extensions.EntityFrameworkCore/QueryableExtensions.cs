using AspNetCoreNuxt.Extensions.Collections;
using AspNetCoreNuxt.Extensions.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore
{
    /// <summary>
    /// <see cref="IQueryable{T}"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// 指定されたソート条件をもとにシーケンスを並び替えます。
        /// </summary>
        /// <typeparam name="T">シーケンスの要素の型。</typeparam>
        /// <param name="source"><see cref="IQueryable{T}"/> オブジェクト。</param>
        /// <param name="sorting">ソート条件を表す <see cref="IQueryableSorting{T}"/> オブジェクト。</param>
        /// <returns><see cref="IQueryable{T}"/> オブジェクト。</returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IQueryableSorting<T> sorting)
            => sorting.SortDirection == SortDirection.Ascending ? source.OrderBy(sorting.SortPropertyExpression) : source.OrderByDescending(sorting.SortPropertyExpression);

        /// <summary>
        /// 指定されたページング条件をもとにシーケンスを非同期でページングします。
        /// </summary>
        /// <typeparam name="T">シーケンスの要素の型。</typeparam>
        /// <param name="source"><see cref="IQueryable{T}"/> オブジェクト。</param>
        /// <param name="paging">ページング条件を表す <see cref="IPaging"/> オブジェクト。</param>
        /// <returns><see cref="IPagination{T}"/> オブジェクト。</returns>
        public static async Task<IPagination<T>> PaginateAsync<T>(this IQueryable<T> source, IPaging paging)
        {
            var zeroBasedPageIndex = paging.PageIndex > 0 ? paging.PageIndex - 1 : 0;
            var skip = paging.PageSize * zeroBasedPageIndex;
            var take = paging.PageSize;

            var count = await source.CountAsync().ConfigureAwait(false);
            var items = count > skip ? await source.Skip(skip).Take(take).ToArrayAsync().ConfigureAwait(false) : Array.Empty<T>();

            return new Pagination<T>()
            {
                CurrentPageIndex = zeroBasedPageIndex + 1,
                CurrentPageItems = items,
                CurrentPageSize = paging.PageSize,
                TotalItemCount = count,
                TotalPageCount = (int)Math.Ceiling(1M * count / paging.PageSize),
            };
        }

        /// <summary>
        /// 指定された仕様をもとにシーケンスをフィルターします。
        /// </summary>
        /// <typeparam name="T">シーケンスの要素の型。</typeparam>
        /// <param name="source"><see cref="IQueryable{T}"/> オブジェクト。</param>
        /// <param name="specification">シーケンスをフィルターする仕様を表す <see cref="ILinqSpecification{T}"/> オブジェクト。</param>
        /// <returns><see cref="IQueryable{T}"/> オブジェクト。</returns>
        public static IQueryable<T> SatisfiedBy<T>(this IQueryable<T> source, ILinqSpecification<T> specification)
            => source.Where(specification.GetExpression());
    }
}
