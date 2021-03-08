using AspNetCoreNuxt.Extensions.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels
{
    /// <summary>
    /// 検索の仕様を表します。
    /// </summary>
    /// <typeparam name="T">コンポーネントの型。</typeparam>
    public abstract class SearchSpecification<T>
    {
        /// <summary>
        /// <see cref="ParameterExpression"/> オブジェクトを表します。
        /// </summary>
        private readonly ParameterExpression DefaultParameterExpression = Expression.Parameter(typeof(T), "x");

        /// <summary>
        /// 検索条件を表す式ツリーのコレクションを表します。
        /// </summary>
        private readonly List<Expression<Func<T, bool>>> Expressions = new();

        /// <summary>
        /// 検索条件を評価する式ツリーを取得します。
        /// </summary>
        /// <returns>検索条件を評価する式ツリー。</returns>
        public Expression<Func<T, bool>> GetExpression()
        {
            if (Expressions.Count == 0)
            {
                return _ => true;
            }
            if (Expressions.Count == 1)
            {
                return Expressions[0];
            }
            var bodyExpression = Expressions.Select(x => x.Body).Aggregate((left, right) => Expression.AndAlso(left, right));
            return Expression.Lambda<Func<T, bool>>(bodyExpression, DefaultParameterExpression);
        }

        /// <summary>
        /// 指定された式ツリーを論理演算子で結合し検索条件として追加します。
        /// </summary>
        /// <param name="logicalOperator">論理演算子を表す <see cref="LogicalOperator"/> 値。</param>
        /// <param name="expressions">検索条件として追加する式ツリーのコレクション。</param>
        protected void Add(LogicalOperator logicalOperator, params Expression<Func<T, bool>>[] expressions)
        {
            Func<Expression, Expression, BinaryExpression> combine = logicalOperator switch
            {
                LogicalOperator.AndAlso => Expression.AndAlso,
                LogicalOperator.OrElse => Expression.OrElse,
                _ => throw new InvalidOperationException(),
            };
            var bodyExpression = expressions
                .Select(expression =>
                {
                    var visitor = new ReplacingExpressionVisitor(expression.Parameters.Single(), DefaultParameterExpression);
                    return visitor.Visit(expression.Body);
                })
                .Aggregate((left, right) => combine(left, right));
            var expression = Expression.Lambda<Func<T, bool>>(bodyExpression, DefaultParameterExpression);
            Expressions.Add(expression);
        }

        /// <summary>
        /// 指定された <see cref="SearchConditions{T}"/> オブジェクトを式ツリーへ変換し検索条件として追加します。
        /// </summary>
        /// <typeparam name="TProperty">プロパティの型。</typeparam>
        /// <param name="conditions"><see cref="SearchConditions{T}"/> オブジェクト。</param>
        protected void TryAdd<TProperty>(SearchConditions<TProperty> conditions)
        {
            if (conditions is null)
            {
                return;
            }
            var propertyExpression = Expression.Property(DefaultParameterExpression, conditions.PropertyName);
            var expressions = conditions
                .Comparisons
                .Select(comparison => ConvertToExpression(propertyExpression, comparison))
                .Select(expression => Expression.Lambda<Func<T, bool>>(expression, DefaultParameterExpression))
                .ToArray();
            Add(conditions.LogicalOperator, expressions);
        }

        /// <summary>
        /// 指定された条件をもとに検索条件を表す式ツリーを作成します。
        /// </summary>
        /// <typeparam name="TProperty">プロパティの型。</typeparam>
        /// <param name="propertyExpression">プロパティへのアクセスを示す式ツリー。</param>
        /// <param name="comparison">比較演算子と比較値を表す <see cref="SearchConditionsComparison{T}"/> オブジェクト。</param>
        /// <returns>検索条件を表す式ツリー。</returns>
        private static Expression ConvertToExpression<TProperty>(MemberExpression propertyExpression, SearchConditionsComparison<TProperty> comparison)
        {
            var valueExpression = ExpressionHelper.Parameterize(comparison.Value);
            if (propertyExpression.Member is PropertyInfo property && property.PropertyType.IsNullable())
            {
                propertyExpression = Expression.Property(propertyExpression, "Value");
            }
            return comparison.ComparisonOperator switch
            {
                ComparisonOperator.Equals
                    => ComparisonExpression.Equals<TProperty>(propertyExpression, valueExpression),
                ComparisonOperator.NotEquals
                    => ComparisonExpression.NotEquals<TProperty>(propertyExpression, valueExpression),
                ComparisonOperator.GreaterThan
                    => ComparisonExpression.GreaterThan<TProperty>(propertyExpression, valueExpression),
                ComparisonOperator.GreaterThanOrEquals
                    => ComparisonExpression.GreaterThanOrEquals<TProperty>(propertyExpression, valueExpression),
                ComparisonOperator.LessThan
                    => ComparisonExpression.LessThan<TProperty>(propertyExpression, valueExpression),
                ComparisonOperator.LessThanOrEquals
                    => ComparisonExpression.LessThanOrEquals<TProperty>(propertyExpression, valueExpression),
                ComparisonOperator.Contains
                    => ComparisonExpression.Contains<TProperty>(propertyExpression, valueExpression),
                ComparisonOperator.NotContains
                    => ComparisonExpression.NotContains<TProperty>(propertyExpression, valueExpression),
                ComparisonOperator.StartsWith
                    => ComparisonExpression.StartsWith<TProperty>(propertyExpression, valueExpression),
                ComparisonOperator.NotStartsWith
                    => ComparisonExpression.NotStartsWith<TProperty>(propertyExpression, valueExpression),
                ComparisonOperator.EndsWith
                    => ComparisonExpression.EndsWith<TProperty>(propertyExpression, valueExpression),
                ComparisonOperator.NotEndsWith
                    => ComparisonExpression.NotEndsWith<TProperty>(propertyExpression, valueExpression),
                ComparisonOperator.IsNull
                    => ComparisonExpression.IsNull<TProperty>(propertyExpression),
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
