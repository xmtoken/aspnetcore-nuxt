using AspNetCoreNuxt.Extensions.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore
{
    /// <summary>
    /// <see cref="IQueryable{T}"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static partial class QueryableExtensions
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> source, IModel model, params object[] keyValues)
        {
            var linq = source;

            var properties = model.FindEntityType(typeof(T)).FindPrimaryKey().Properties;
            for (var i = 0; i < properties.Count; i++)
            {
                var property = properties[i];
                var keyValue = keyValues[i];

                var parameterExpression = Expression.Parameter(typeof(T));
                var leftExpression = Expression.Property(parameterExpression, property.Name);

                var rightParameterizedExpression = ExpressionHelper.Parameterize(keyValue);
                var rightExpression = Expression.Convert(rightParameterizedExpression, property.ClrType);

                var bodyExpression = Expression.Equal(leftExpression, rightExpression);
                var expression = Expression.Lambda<Func<T, bool>>(bodyExpression, parameterExpression);

                linq = linq.Where(expression);
            }

            return linq;
        }
    }
}
