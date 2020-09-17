using System;
using System.Linq.Expressions;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore
{
    public partial class LinqSpecification<T>
    {
        /// <inheritdoc cref="CompareInternal{TProperty}(Expression{Func{T, TProperty}}, TProperty, ValueComparisonOperator?)"/>
        protected Expression<Func<T, bool>> Compare<TProperty>(Expression<Func<T, TProperty>> propertyExpression, TProperty value, ValueComparisonOperator? comparison)
            where TProperty : struct
            => CompareInternal(propertyExpression, value, comparison);

        /// <inheritdoc cref="CompareInternal{TProperty}(Expression{Func{T, TProperty}}, TProperty, ValueComparisonOperator?)"/>
        protected Expression<Func<T, bool>> Compare<TProperty>(Expression<Func<T, TProperty?>> propertyExpression, TProperty? value, ValueComparisonOperator? comparison)
            where TProperty : struct
            => CompareInternal(propertyExpression, value, comparison);

        /// <summary>
        /// 指定された値と比較演算子をもとに値の比較を行う式ツリーを返します。
        /// </summary>
        /// <typeparam name="TProperty">比較するプロパティの型。</typeparam>
        /// <param name="propertyExpression">比較するプロパティを示す式ツリー。</param>
        /// <param name="value">比較する値。</param>
        /// <param name="comparison">比較演算子を表す <see cref="ValueComparisonOperator"/> 値。</param>
        /// <returns>値の比較を行う式ツリー。</returns>
        private static Expression<Func<T, bool>> CompareInternal<TProperty>(Expression<Func<T, TProperty>> propertyExpression, TProperty value, ValueComparisonOperator? comparison)
            => comparison switch
            {
                ValueComparisonOperator.Equals
                    => CompareByOperator(propertyExpression, value, Expression.Equal),
                ValueComparisonOperator.NotEquals
                    => CompareByOperator(propertyExpression, value, Expression.NotEqual),
                ValueComparisonOperator.GreaterThan
                    => CompareByOperator(propertyExpression, value, Expression.GreaterThan),
                ValueComparisonOperator.GreaterThanOrEquals
                    => CompareByOperator(propertyExpression, value, Expression.GreaterThanOrEqual),
                ValueComparisonOperator.LessThan
                    => CompareByOperator(propertyExpression, value, Expression.LessThan),
                ValueComparisonOperator.LessThanOrEquas
                    => CompareByOperator(propertyExpression, value, Expression.LessThanOrEqual),
                _
                    => CompareByOperator(propertyExpression, value, Expression.Equal),
            };
    }
}
