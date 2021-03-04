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
        /// 指定されたプロパティ名をプロパティへのアクセスを表す式ツリーへ変換できるかどうかを返します。
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
        /// 指定されたプロパティ名をプロパティへのアクセスを表す式ツリーへ変換します。
        /// </summary>
        /// <typeparam name="T">コンポーネントの型。</typeparam>
        /// <param name="chainPropertyName">ピリオドで区切られたプロパティ名。</param>
        /// <returns>プロパティへのアクセスを表す式ツリー。</returns>
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
        /// 指定されたプロパティ名が <see cref="IEnumerable"/> インターフェイスのプロパティを含むかどうかを返します。
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
        /// 指定されたプロパティ名を正規化します。
        /// </summary>
        /// <typeparam name="T">コンポーネントの型。</typeparam>
        /// <param name="chainPropertyName">ピリオドで区切られたプロパティ名。</param>
        /// <returns>正規化されたプロパティ名。</returns>
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
        /// 指定されたリテラル値を匿名オブジェクトのプロパティへのアクセスを表す式ツリーへ変換します。
        /// </summary>
        /// <typeparam name="T">匿名オブジェクトでオブジェクト化する値の型。</typeparam>
        /// <param name="value">匿名オブジェクトでオブジェクト化する値。</param>
        /// <returns>匿名オブジェクトのプロパティへのアクセスを表す式ツリー。</returns>
        public static MemberExpression Parameterize<T>(T value)
        {
            var component = new { Value = value };
            return Expression.Property(Expression.Constant(component), nameof(component.Value));
        }

        /// <summary>
        /// 指定されたプロパティ名が有効かどうかを返します。
        /// </summary>
        /// <typeparam name="T">コンポーネントの型。</typeparam>
        /// <param name="chainPropertyName">ピリオドで区切られたプロパティ名。</param>
        /// <returns>指定されたプロパティ名が有効な場合は true。それ以外の場合は false。</returns>
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

        /// <summary>
        /// 指定されたオブジェクト初期化子の式ツリーをすべてのメンバーの初期化子を含めた式ツリーへ変換します。
        /// </summary>
        /// <typeparam name="T">コンポーネントの型。</typeparam>
        /// <param name="expression"></param>
        /// <returns>すべてのメンバーの初期化子を含めた式ツリー。</returns>
        /// <remarks>概念としてはレコード型 (C# 9.0) の with 式と同じです。</remarks>
        public static Expression<Func<T, T>> With<T>(Expression<Func<T, T>> expression)
        {
            var parameterExpression = Expression.Parameter(typeof(T), "x");

            var visitor = new ReplacingExpressionVisitor(expression.Parameters.Single(), parameterExpression);
            var visitorExpression = visitor.Visit(expression.Body);
            var sourceInitExpression = (MemberInitExpression)visitorExpression;

            var bindings = new List<MemberAssignment>();

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
                var assignmentExpression = Expression.Bind(assignment.Member, assignment.Expression);
                bindings.Add(assignmentExpression);
            }

            var newExpression = Expression.New(typeof(T));
            var initExpression = Expression.MemberInit(newExpression, bindings);

            return (Expression<Func<T, T>>)Expression.Lambda(initExpression, parameterExpression);
        }
    }
}
