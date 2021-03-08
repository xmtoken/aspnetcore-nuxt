using AspNetCoreNuxt.Extensions.Collections;
using AspNetCoreNuxt.Extensions.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels
{
    /// <inheritdoc cref="ISortQuery{T}"/>
    internal class SortQuery<T> : ISortQuery<T>
    {
        /// <summary>
        /// バインディングするモデルの名前を表します。
        /// </summary>
        internal const string BindingModelName = "$sort";

        /// <summary>
        /// <see cref="SortQuery{T}"/> クラスの既定のインスタンスを取得します。
        /// </summary>
        internal static SortQuery<T> Default { get; } = new SortQuery<T>(Array.Empty<ISorting>(), Array.Empty<string>());

        /// <inheritdoc/>
        IEnumerable<string> ISortQuery<T>.PropertyNames => Sortings.Select(x => x.PropertyName);

        /// <summary>
        /// <see cref="ISorting"/> オブジェクトのコレクションを取得します。
        /// </summary>
        public IReadOnlyCollection<ISorting> Sortings { get; }

        /// <summary>
        /// クエリストリングで指定された値を取得します。
        /// </summary>
        public IReadOnlyCollection<string> PrimitiveValues { get; }

        /// <summary>
        /// <see cref="SortQuery{T}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="sortings"><see cref="ISorting"/> オブジェクトのコレクション。</param>
        /// <param name="primitiveValues">クエリストリングで指定された値。</param>
        public SortQuery(ISorting[] sortings, string[] primitiveValues)
        {
            Sortings = Array.AsReadOnly(sortings);
            PrimitiveValues = Array.AsReadOnly(primitiveValues);
        }

        /// <inheritdoc/>
        public Func<IQueryable<T>, IQueryable<T>> GetSortMethod()
        {
            return source =>
            {
                var linq = source as IOrderedQueryable<T>;
                var index = 0;
                foreach (var sorting in Sortings)
                {
                    var expression = ExpressionHelper.ConvertToExpression<T>(sorting.PropertyName);
                    linq = sorting.SortDirection == SortDirection.Ascending
                         ? index++ == 0 ? linq.OrderBy(expression) : linq.ThenBy(expression)
                         : index++ == 0 ? linq.OrderByDescending(expression) : linq.ThenByDescending(expression);
                }
                return linq;
            };
        }
    }
}
