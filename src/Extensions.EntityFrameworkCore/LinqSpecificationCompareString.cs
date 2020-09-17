using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore
{
    public partial class LinqSpecification<T>
    {
        /// <summary>
        /// <see cref="string.CompareTo(string)"/> メソッドを表します。
        /// </summary>
        private static readonly MethodInfo StringCompareToMethod
            = typeof(string).GetMethod(nameof(string.CompareTo), new[] { typeof(string) });

        /// <summary>
        /// <see cref="string.Contains(string)"/> メソッドを表します。
        /// </summary>
        private static readonly MethodInfo StringContainsMethod
            = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });

        /// <summary>
        /// <see cref="string.StartsWith(string)"/> メソッドを表します。
        /// </summary>
        private static readonly MethodInfo StringStartsWithMethod
            = typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) });

        /// <summary>
        /// <see cref="string.EndsWith(string)"/> メソッドを表します。
        /// </summary>
        private static readonly MethodInfo StringEndsWithMethod
            = typeof(string).GetMethod(nameof(string.EndsWith), new[] { typeof(string) });

        /// <inheritdoc cref="CompareInternal(Expression{Func{T, string}}, string, StringComparisonOperator?)"/>
        protected Expression<Func<T, bool>> Compare(Expression<Func<T, string>> propertyExpression, string value, StringComparisonOperator? comparison)
            => CompareInternal(propertyExpression, value, comparison);

        /// <summary>
        /// 指定された文字列と比較演算子をもとに文字列の比較を行う式ツリーを返します。
        /// </summary>
        /// <param name="propertyExpression">比較するプロパティを示す式ツリー。</param>
        /// <param name="value">比較する文字列。</param>
        /// <param name="comparison">比較演算子を表す <see cref="StringComparisonOperator"/> 値。</param>
        /// <returns>文字列の比較を行う式ツリー。</returns>
        private static Expression<Func<T, bool>> CompareInternal(Expression<Func<T, string>> propertyExpression, string value, StringComparisonOperator? comparison)
            => comparison switch
            {
                StringComparisonOperator.Equals
                    => CompareByOperator(propertyExpression, value, Expression.Equal),
                StringComparisonOperator.NotEquals
                    => CompareByOperator(propertyExpression, value, Expression.NotEqual),
                StringComparisonOperator.GreaterThan
                    => CompareByMethodAndOperator(propertyExpression, value, StringCompareToMethod, Expression.GreaterThan),
                StringComparisonOperator.GreaterThanOrEquals
                    => CompareByMethodAndOperator(propertyExpression, value, StringCompareToMethod, Expression.GreaterThanOrEqual),
                StringComparisonOperator.LessThan
                    => CompareByMethodAndOperator(propertyExpression, value, StringCompareToMethod, Expression.LessThan),
                StringComparisonOperator.LessThanOrEquas
                    => CompareByMethodAndOperator(propertyExpression, value, StringCompareToMethod, Expression.LessThanOrEqual),
                StringComparisonOperator.Contains
                    => CompareByMethod(propertyExpression, value, StringContainsMethod),
                StringComparisonOperator.StartsWith
                    => CompareByMethod(propertyExpression, value, StringStartsWithMethod),
                StringComparisonOperator.EndsWith
                    => CompareByMethod(propertyExpression, value, StringEndsWithMethod),
                _
                    => CompareByOperator(propertyExpression, value, Expression.Equal),
            };
    }
}
