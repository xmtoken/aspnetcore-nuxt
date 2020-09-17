using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore
{
    public partial class LinqSpecification<T>
    {
        /// <summary>
        /// 指定された値とメソッドをもとに値の比較を行う式ツリーを返します。
        /// </summary>
        /// <typeparam name="TProperty">比較するプロパティの型。</typeparam>
        /// <param name="propertyExpression">比較するプロパティを示す式ツリー。</param>
        /// <param name="value">比較する値。</param>
        /// <param name="compareMethod">値の比較を行うメソッドを表す <see cref="MethodInfo"/> オブジェクト。</param>
        /// <returns>値の比較を行う式ツリー。</returns>
        private static Expression<Func<T, bool>> CompareByMethod<TProperty>(Expression<Func<T, TProperty>> propertyExpression, TProperty value, MethodInfo compareMethod)
        {
            var expression = Expression.Call(propertyExpression.Body, compareMethod, Expression.Constant(value));
            return Expression.Lambda<Func<T, bool>>(expression, propertyExpression.Parameters);
        }

        /// <summary>
        /// 指定された値とメソッド、デリゲートをもとに値の比較を行う式ツリーを返します。
        /// </summary>
        /// <typeparam name="TProperty">比較するプロパティの型。</typeparam>
        /// <param name="propertyExpression">比較するプロパティを示す式ツリー。</param>
        /// <param name="value">比較する値。</param>
        /// <param name="compareMethod">値の比較を行うメソッドを表す <see cref="MethodInfo"/> オブジェクト。</param>
        /// <param name="operateExpressionCreator">値の比較を行う式ツリーを返すデリゲート。</param>
        /// <returns>値の比較を行う式ツリー。</returns>
        private static Expression<Func<T, bool>> CompareByMethodAndOperator<TProperty>(Expression<Func<T, TProperty>> propertyExpression, TProperty value, MethodInfo compareMethod, Func<Expression, Expression, BinaryExpression> operateExpressionCreator)
        {
            var expressionMethod = Expression.Call(propertyExpression.Body, compareMethod, Expression.Constant(value));
            var expression = operateExpressionCreator(expressionMethod, Expression.Constant(0));
            return Expression.Lambda<Func<T, bool>>(expression, propertyExpression.Parameters);
        }

        /// <summary>
        /// 指定された値とデリゲートをもとに値の比較を行う式ツリーを返します。
        /// </summary>
        /// <typeparam name="TProperty">比較するプロパティの型。</typeparam>
        /// <param name="propertyExpression">比較するプロパティを示す式ツリー。</param>
        /// <param name="value">比較する値。</param>
        /// <param name="operateExpressionCreator">値の比較を行う式ツリーを返すデリゲート。</param>
        /// <returns>値の比較を行う式ツリー。</returns>
        private static Expression<Func<T, bool>> CompareByOperator<TProperty>(Expression<Func<T, TProperty>> propertyExpression, TProperty value, Func<Expression, Expression, BinaryExpression> operateExpressionCreator)
        {
            var expression = operateExpressionCreator(propertyExpression.Body, Expression.Constant(value));
            return Expression.Lambda<Func<T, bool>>(expression, propertyExpression.Parameters);
        }
    }
}
