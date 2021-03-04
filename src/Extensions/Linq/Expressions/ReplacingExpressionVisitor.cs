using System.Linq.Expressions;

namespace AspNetCoreNuxt.Extensions.Linq.Expressions
{
    /// <summary>
    /// 指定されたインスタンスの式ツリーを別の式ツリーへ差し替えるビジターを表します。
    /// </summary>
    public class ReplacingExpressionVisitor : ExpressionVisitor
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
