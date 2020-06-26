using AspNetCoreNuxt.Extensions.Collections;
using AspNetCoreNuxt.Extensions.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore
{
    /// <inheritdoc cref="IQueryableSorting{T}"/>
    public class QueryableSorting<T> : IQueryableSorting<T>
    {
        /// <inheritdoc/>
        public SortDirection SortDirection { get; }

        /// <inheritdoc/>
        public Expression<Func<T, object>> SortPropertyExpression { get; }

        /// <inheritdoc/>
        public string SortPropertyName { get; }

        /// <summary>
        /// <see cref="QueryableSorting{T}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="propertyName">並び替えのキーとなるプロパティの名前。</param>
        /// <param name="direction">並び替え方向。</param>
        public QueryableSorting(string propertyName, SortDirection direction)
        {
            SortPropertyName = propertyName;
            SortDirection = direction;
            SortPropertyExpression = ConvertToExpression(propertyName);

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

        /// <summary>
        /// <see cref="QueryableSorting{T}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="expression">並び替えのキーとなるプロパティを示す式ツリー。</param>
        /// <param name="direction">並び替え方向。</param>
        public QueryableSorting(Expression<Func<T, object>> expression, SortDirection direction)
        {
            SortPropertyExpression = expression;
            SortDirection = direction;

            var node
                = expression.Body.NodeType == ExpressionType.Convert || expression.NodeType == ExpressionType.ConvertChecked
                ? expression.Body<UnaryExpression>().Operand<MemberExpression>()
                : expression.Body<MemberExpression>();

            var properties = new List<string>();
            while (node != null)
            {
                properties.Add(node.Member.Name);
                node = node.Expression as MemberExpression;
            }

            SortPropertyName = string.Join('.', properties.AsEnumerable().Reverse());
        }
    }
}
