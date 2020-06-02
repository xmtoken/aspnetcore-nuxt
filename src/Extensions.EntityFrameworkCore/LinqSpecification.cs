using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore
{
    /// <inheritdoc cref="ILinqSpecification{T}"/>
    public abstract class LinqSpecification<T> : ILinqSpecification<T>
    {
        /// <inheritdoc/>
        public bool IsSatisfiedBy(T obj)
            => GetExpression().Compile()(obj);

        /// <inheritdoc/>
        public Expression<Func<T, bool>> GetExpression()
        {
            var expressions = GetExpressions()?.ToArray();
            switch (expressions?.Length ?? 0)
            {
                case 0:
                    return _ => true;

                case 1:
                    return expressions[0];

                default:
                    var parameter = Expression.Parameter(typeof(T), "x");
                    var body = expressions
                        .Select(expression =>
                        {
                            var visitor = new ReplacingExpressionVisitor(expression.Parameters.Single(), parameter);
                            return visitor.Visit(expression.Body);
                        })
                        .Aggregate((left, right) => Expression.AndAlso(left, right));
                    return Expression.Lambda<Func<T, bool>>(body, parameter);
            }
        }

        /// <summary>
        /// オブジェクトが仕様を満たしているかどうかを検証する式ツリーのコレクションを取得します。
        /// </summary>
        /// <returns>オブジェクトが仕様を満たしているかどうかを検証する式ツリーのコレクション。</returns>
        protected abstract IEnumerable<Expression<Func<T, bool>>> GetExpressions();

        /// <summary>
        /// 指定された式ツリーを別の式ツリーで差し替えるビジターを表します。
        /// </summary>
        private class ReplacingExpressionVisitor : ExpressionVisitor
        {
            /// <summary>
            /// 差し替える対象となる式ツリーを表します。
            /// </summary>
            private readonly Expression Original;

            /// <summary>
            /// 対象と差し替える式ツリーを表します。
            /// </summary>
            private readonly Expression Replacement;

            /// <summary>
            /// <see cref="ReplacingExpressionVisitor"/> クラスの新しいインスタンスを作成します。
            /// </summary>
            /// <param name="original">差し替える対象となる式ツリー。</param>
            /// <param name="replacement">対象と差し替える式ツリー。</param>
            public ReplacingExpressionVisitor(Expression original, Expression replacement)
            {
                Original = original;
                Replacement = replacement;
            }

            /// <inheritdoc/>
            public override Expression Visit(Expression node)
                => node == Original ? Replacement : base.Visit(node);
        }
    }
}
