using System.Linq.Expressions;

namespace AspNetCoreNuxt.Extensions.Linq.Expressions
{
    /// <summary>
    /// <see cref="UnaryExpression"/> クラスの拡張機能を提供します。
    /// </summary>
    public static class UnaryExpressionExtensions
    {
        /// <summary>
        /// 単項演算式のオペランドを表す式ツリーを取得します。
        /// </summary>
        /// <typeparam name="TExpression">単項演算式のオペランドを表す式ツリーの型。</typeparam>
        /// <param name="expression"><see cref="UnaryExpression"/> オブジェクト。</param>
        /// <returns><typeparamref name="TExpression"/> オブジェクト。</returns>
        public static TExpression Operand<TExpression>(this UnaryExpression expression)
            where TExpression : Expression
            => (TExpression)expression.Operand;
    }
}
