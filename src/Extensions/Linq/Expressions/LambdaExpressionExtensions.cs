using System.Linq.Expressions;

namespace AspNetCoreNuxt.Extensions.Linq.Expressions
{
    /// <summary>
    /// <see cref="LambdaExpression"/> クラスの拡張機能を提供します。
    /// </summary>
    public static class LambdaExpressionExtensions
    {
        /// <summary>
        /// ラムダ式の本体を表す式ツリーを取得します。
        /// </summary>
        /// <typeparam name="TExpression">ラムダ式の本体を表す式ツリーの型。</typeparam>
        /// <param name="expression"><see cref="LambdaExpression"/> オブジェクト。</param>
        /// <returns><typeparamref name="TExpression"/> オブジェクト。</returns>
        public static TExpression Body<TExpression>(this LambdaExpression expression)
            where TExpression : Expression
            => (TExpression)expression.Body;
    }
}
