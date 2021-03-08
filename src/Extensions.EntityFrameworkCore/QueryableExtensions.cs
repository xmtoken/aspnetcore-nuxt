using AspNetCoreNuxt.Extensions.Collections;
using AspNetCoreNuxt.Extensions.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore
{
    /// <summary>
    /// <see cref="IQueryable{T}"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static partial class QueryableExtensions
    {
        ///// <summary>
        ///// 指定されたソート条件をもとにシーケンスを並び替えます。
        ///// </summary>
        ///// <typeparam name="T">シーケンスの要素の型。</typeparam>
        ///// <param name="source"><see cref="IQueryable{T}"/> オブジェクト。</param>
        ///// <param name="sorting">ソート条件を表す <see cref="IQueryableSorting{T}"/> オブジェクト。</param>
        ///// <returns><see cref="IQueryable{T}"/> オブジェクト。</returns>
        //public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IQueryableSorting<T> sorting)
        //    => source.OrderBy(new[] { sorting });

        ///// <summary>
        ///// 指定されたソート条件をもとにシーケンスを並び替えます。
        ///// </summary>
        ///// <typeparam name="T">シーケンスの要素の型。</typeparam>
        ///// <param name="source"><see cref="IQueryable{T}"/> オブジェクト。</param>
        ///// <param name="sortings">ソート条件を表す <see cref="IQueryableSorting{T}"/> オブジェクトのコレクション。</param>
        ///// <returns><see cref="IQueryable{T}"/> オブジェクト。</returns>
        //public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IEnumerable<IQueryableSorting<T>> sortings)
        //{
        //    if (sortings.FirstOrDefault() is IQueryableSorting<T> sorting)
        //    {
        //        var ordered
        //            = sorting.SortDirection == SortDirection.Ascending
        //            ? source.OrderBy(sorting.SortPropertyExpression)
        //            : source.OrderByDescending(sorting.SortPropertyExpression);

        //        return sortings.Skip(1).Aggregate(ordered, (query, sorting)
        //            => sorting.SortDirection == SortDirection.Ascending
        //             ? query.ThenBy(sorting.SortPropertyExpression)
        //             : query.ThenByDescending(sorting.SortPropertyExpression));
        //    }
        //    return source;
        //}

        public static IOrderedQueryable<T> SortBy<T>(this IQueryable<T> source, IEnumerable<(Expression<Func<T, object>> Expression, SortDirection Direction)> sortings)
        {
            return SortBy(source, sortings.ToArray());
        }

        public static IOrderedQueryable<T> SortBy<T>(this IQueryable<T> source, params (Expression<Func<T, object>> Expression, SortDirection Direction)[] sortings)
        {
            return sortings.Skip(1).Aggregate(source.SortBy(sortings[0].Expression, sortings[0].Direction), (query, sorting)
                => query.SortBy(sorting.Expression, sorting.Direction));
        }

        public static IOrderedQueryable<T> SortBy<T>(this IOrderedQueryable<T> source, Expression<Func<T, object>> expression, SortDirection direction)
        {
            return direction == SortDirection.Ascending
                ? source.ThenBy(expression)
                : source.ThenByDescending(expression);
        }

        public static IOrderedQueryable<T> SortBy<T>(this IQueryable<T> source, Expression<Func<T, object>> expression, SortDirection direction)
        {
            return direction == SortDirection.Ascending
                ? source.OrderBy(expression)
                : source.OrderByDescending(expression);
        }

        public static IQueryable<T> OrderByPrimaryKey<T>(this IQueryable<T> source, IModel model)
        {
            var properties = model.FindEntityType(typeof(T)).FindPrimaryKey().Properties;

            var ordered = source.OrderBy(ConvertToExpression(properties[0].Name));

            return properties.Skip(1).Aggregate(ordered, (query, property)
                => query.ThenBy(ConvertToExpression(property.Name)));

            static Expression<Func<T, object>> ConvertToExpression(string propertyName)
            {
                if (string.IsNullOrEmpty(propertyName))
                {
                    return null;
                }

                var type = typeof(T);
                var parameter = Expression.Parameter(type);
                var body = (Expression)parameter;

                foreach (var name in propertyName.Split('.'))
                {
                    var property = type.GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                    if (property == null)
                    {
                        return null;
                    }

                    type = property.PropertyType;
                    body = Expression.Property(body, property.Name);
                }

                body = Expression.Convert(body, typeof(object));
                return Expression.Lambda<Func<T, object>>(body, parameter);
            }
        }

        ///// <summary>
        ///// 指定されたページング条件をもとにシーケンスを非同期でページングします。
        ///// </summary>
        ///// <typeparam name="T">シーケンスの要素の型。</typeparam>
        ///// <param name="source"><see cref="IQueryable{T}"/> オブジェクト。</param>
        ///// <param name="paging">ページング条件を表す <see cref="IPaging"/> オブジェクト。</param>
        ///// <returns><see cref="IPagination{T}"/> オブジェクト。</returns>
        //public static async Task<IPagination<T>> PaginateAsync<T>(this IQueryable<T> source, IPaging paging)
        //{
        //    var zeroBasedPageIndex = paging.PageIndex > 0 ? paging.PageIndex - 1 : 0;
        //    var skip = paging.PageSize * zeroBasedPageIndex;
        //    var take = paging.PageSize;

        //    var count = await source.CountAsync().ConfigureAwait(false);
        //    var items = count > skip ? await source.Skip(skip).Take(take).ToArrayAsync().ConfigureAwait(false) : Array.Empty<T>();

        //    return new Pagination<T>()
        //    {
        //        CurrentPageIndex = zeroBasedPageIndex + 1,
        //        CurrentPageItems = items,
        //        CurrentPageSize = paging.PageSize,
        //        TotalItemCount = count,
        //        TotalPageCount = (int)Math.Ceiling(1M * count / paging.PageSize),
        //    };
        //}

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
