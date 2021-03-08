using AspNetCoreNuxt.Extensions.Linq.Expressions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore
{
    /// <summary>
    /// <see cref="IQueryable{T}"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class QueryableExtensionsWith
    {
        public static IQueryable<TSource> SelectWith<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, TSource>> selector)
            => source.Select(ExpressionHelper.With(selector));
    }
}
