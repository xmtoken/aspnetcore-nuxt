using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AspNetCoreNuxt.Extensions.Linq.Expressions
{
    /// <summary>
    /// <see cref="Expression"/> クラスに関する機能を提供します。
    /// </summary>
    public static class ExpressionHelper
    {
        /// <summary>
        /// 指定されたプロパティ名をコンポーネントの型を基準としたプロパティへのアクセスを表す式ツリーへ変換できるかどうかを返します。
        /// </summary>
        /// <typeparam name="T">コンポーネントの型。</typeparam>
        /// <param name="chainPropertyName">ピリオドで区切られたプロパティ名。</param>
        /// <returns>式ツリーへ変換できる場合は true。それ以外の場合は false。</returns>
        /// <remarks>コレクション型のプロパティを含む式ツリーへの変換は行えません。</remarks>
        public static bool CanConvertToExpression<T>(string chainPropertyName)
        {
            if (string.IsNullOrEmpty(chainPropertyName))
            {
                return false;
            }

            var breaked = false;
            var componentType = typeof(T);

            foreach (var propertyName in chainPropertyName.Split('.'))
            {
                var property = componentType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                if (property is null || breaked)
                {
                    return false;
                }
                else if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                {
                    breaked = true;
                }
                else if (property.PropertyType.GetInterface(typeof(IEnumerable).Name) is not null)
                {
                    return false;
                }
                else
                {
                    componentType = property.PropertyType;
                }
            }

            return breaked;
        }

        /// <summary>
        /// 指定されたプロパティ名をコンポーネントの型を基準としたプロパティへのアクセスを表す式ツリーへ変換します。
        /// </summary>
        /// <typeparam name="T">コンポーネントの型。</typeparam>
        /// <param name="chainPropertyName">ピリオドで区切られたプロパティ名。</param>
        /// <returns>コンポーネントの型を基準としたプロパティへのアクセスを表す式ツリー。</returns>
        /// <remarks>コレクション型のプロパティを含む式ツリーへの変換は行えません。</remarks>
        public static Expression<Func<T, object>> ConvertToExpression<T>(string chainPropertyName)
        {
            var breaked = false;
            var componentType = typeof(T);

            var parameterExpression = Expression.Parameter(componentType);
            var bodyExpression = (Expression)parameterExpression;

            foreach (var propertyName in chainPropertyName.Split('.'))
            {
                var property = componentType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                if (property is null || breaked)
                {
                    throw new InvalidOperationException();
                }
                else if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                {
                    bodyExpression = Expression.Property(bodyExpression, property.Name);
                    breaked = true;
                }
                else if (property.PropertyType.GetInterface(typeof(IEnumerable).Name) is not null)
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    bodyExpression = Expression.Property(bodyExpression, property.Name);
                }
            }

            if (!breaked)
            {
                throw new InvalidOperationException();
            }

            bodyExpression = Expression.Convert(bodyExpression, typeof(object));
            return Expression.Lambda<Func<T, object>>(bodyExpression, parameterExpression);
        }

        /// <summary>
        /// 指定されたプロパティ名がコンポーネントの型で <see cref="IEnumerable"/> インターフェイスのプロパティを含むかどうかを返します。
        /// </summary>
        /// <typeparam name="T">コンポーネントの型。</typeparam>
        /// <param name="chainPropertyName">ピリオドで区切られたプロパティ名。</param>
        /// <returns><see cref="IEnumerable"/> インターフェイスのプロパティを含む場合は true。それ以外の場合は false。</returns>
        public static bool HasEnumerable<T>(string chainPropertyName)
        {
            var componentType = typeof(T);

            foreach (var propertyName in chainPropertyName.Split('.'))
            {
                var property = componentType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                if (property is null)
                {
                    throw new InvalidOperationException();
                }
                else if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                {
                    return false;
                }
                else if (property.PropertyType.GetInterface(typeof(IEnumerable).Name) is not null)
                {
                    return true;
                }
                else
                {
                    componentType = property.PropertyType;
                }
            }

            return false;
        }

        /// <summary>
        /// 指定されたコンポーネントの型をもとにプロパティ名を正規化します。
        /// </summary>
        /// <typeparam name="T">コンポーネントの型。</typeparam>
        /// <param name="chainPropertyName">ピリオドで区切られたプロパティ名。</param>
        /// <returns>コンポーネントの型をもとに正規化されたプロパティ名。</returns>
        public static string Normalize<T>(string chainPropertyName)
        {
            var breaked = false;
            var componentType = typeof(T);
            var propertyNames = new List<string>();

            foreach (var propertyName in chainPropertyName.Split('.'))
            {
                var property = componentType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                if (property is null || breaked)
                {
                    throw new InvalidOperationException();
                }
                else if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                {
                    breaked = true;
                    propertyNames.Add(property.Name);
                }
                else if (property.PropertyType.GetInterface(typeof(IEnumerable<>).Name) is Type interfaceType)
                {
                    componentType = interfaceType.GenericTypeArguments[0];
                    propertyNames.Add(property.Name);
                }
                else
                {
                    componentType = property.PropertyType;
                    propertyNames.Add(property.Name);
                }
            }

            if (!breaked)
            {
                throw new InvalidOperationException();
            }

            return string.Join(".", propertyNames);
        }

        /// <summary>
        /// 指定されたリテラル値をオブジェクトで内包しパラメーター化された値へのアクセスを表す式ツリーへ変換します。
        /// </summary>
        /// <typeparam name="T">パラメーター化する値の型。</typeparam>
        /// <param name="value">パラメーター化する値。</param>
        /// <returns>パラメーター化された値へのアクセスを表す式ツリー。</returns>
        public static MemberExpression Parameterize<T>(T value)
        {
            var component = new { Value = value };
            return Expression.Property(Expression.Constant(component), nameof(component.Value));
        }

        /// <summary>
        /// 指定されたプロパティ名がコンポーネントの型で有効かどうかを返します。
        /// </summary>
        /// <typeparam name="T">コンポーネントの型。</typeparam>
        /// <param name="chainPropertyName">ピリオドで区切られたプロパティ名。</param>
        /// <returns>指定されたプロパティ名がコンポーネントの型で有効な場合は true。それ以外の場合は false。</returns>
        public static bool Validate<T>(string chainPropertyName)
        {
            if (string.IsNullOrEmpty(chainPropertyName))
            {
                return false;
            }

            var breaked = false;
            var componentType = typeof(T);

            foreach (var propertyName in chainPropertyName.Split('.'))
            {
                var property = componentType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                if (property is null || breaked)
                {
                    return false;
                }
                else if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                {
                    breaked = true;
                }
                else if (property.PropertyType.GetInterface(typeof(IEnumerable<>).Name) is Type interfaceType)
                {
                    componentType = interfaceType.GenericTypeArguments[0];
                }
                else
                {
                    componentType = property.PropertyType;
                }
            }

            return breaked;
        }






        public static Expression<Func<T, T>> With<T>(Expression<Func<T, T>> expression)
        {
            var bindings = new List<MemberAssignment>();
            var sourceInitExpression = (MemberInitExpression)expression.Body;

            var parameterExpression = Expression.Parameter(typeof(T), "x");

            foreach (var property in typeof(T).GetProperties().Where(x => x.CanRead && x.CanWrite))
            {
                if (sourceInitExpression.Bindings.All(x => x.Member.Name != property.Name))
                {
                    var propertyExpression = Expression.Property(parameterExpression, property.Name);
                    var assignmentExpression = Expression.Bind(property, propertyExpression);
                    bindings.Add(assignmentExpression);
                }
            }

            foreach (var assignment in sourceInitExpression.Bindings.Cast<MemberAssignment>())
            {
                var visitor = new ReplacingParameterExpressionVisitor<T>(parameterExpression);
                var visitExpression = visitor.Visit(assignment.Expression);

                var assignmentExpression = Expression.Bind(assignment.Member, visitExpression);
                bindings.Add(assignmentExpression);
            }

            var newExpression = Expression.New(typeof(T));
            var initExpression = Expression.MemberInit(newExpression, bindings);

            return (Expression<Func<T, T>>)Expression.Lambda(initExpression, parameterExpression);
        }

        public static Expression<Func<T1, T2, T1>> With<T1, T2>(Expression<Func<T1, T2, T1>> expression)
        {
            var bindings = new List<MemberAssignment>();
            var sourceInitExpression = (MemberInitExpression)expression.Body;

            var parameter1Expression = Expression.Parameter(typeof(T1), "x1");
            var parameter2Expression = Expression.Parameter(typeof(T2), "x2");

            foreach (var property in typeof(T1).GetProperties().Where(x => x.CanRead && x.CanWrite))
            {
                if (sourceInitExpression.Bindings.All(x => x.Member.Name != property.Name))
                {
                    var propertyExpression = Expression.Property(parameter1Expression, property.Name);
                    var assignmentExpression = Expression.Bind(property, propertyExpression);
                    bindings.Add(assignmentExpression);
                }
            }

            foreach (var assignment in sourceInitExpression.Bindings.Cast<MemberAssignment>())
            {
                var visitor1 = new ReplacingParameterExpressionVisitor<T1>(parameter1Expression);
                var visitExpression1 = visitor1.Visit(assignment.Expression);

                var visitor2 = new ReplacingParameterExpressionVisitor<T2>(parameter2Expression);
                var visitExpression2 = visitor2.Visit(visitExpression1);

                var assignmentExpression = Expression.Bind(assignment.Member, visitExpression2);
                bindings.Add(assignmentExpression);
            }

            var newExpression = Expression.New(typeof(T1));
            var initExpression = Expression.MemberInit(newExpression, bindings);

            return (Expression<Func<T1, T2, T1>>)Expression.Lambda(initExpression, parameter1Expression, parameter2Expression);
        }

        private class ReplacingParameterExpressionVisitor<T> : ExpressionVisitor
        {
            /// <summary>
            /// 対象と差し替える式ツリーを表します。
            /// </summary>
            private readonly Expression Replacement;

            /// <summary>
            /// <see cref="ReplacingParameterExpressionVisitor{T}"/> クラスの新しいインスタンスを作成します。
            /// </summary>
            /// <param name="replacement">対象と差し替える式ツリー。</param>
            public ReplacingParameterExpressionVisitor(Expression replacement)
            {
                Replacement = replacement;
            }

            /// <inheritdoc/>
            public override Expression Visit(Expression node)
                => node?.NodeType == ExpressionType.Parameter && node.Type == typeof(T) ? Replacement : base.Visit(node);
        }
    }
}
