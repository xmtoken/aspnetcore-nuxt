using AspNetCoreNuxt.Extensions.Reflection;
using System;
using System.Linq.Expressions;

namespace AspNetCoreNuxt.Extensions.Linq.Expressions
{
    /// <summary>
    /// 比較を行う式ツリーに関する機能を提供します。
    /// </summary>
    public static class ExpressionComparer
    {
        /// <summary>
        /// 左辺の値が右辺の値と等しいことを評価する式ツリーを返します。
        /// </summary>
        /// <typeparam name="TProperty">左辺のプロパティの型。</typeparam>
        /// <param name="instance">左辺のプロパティへのアクセスを表す式ツリー。</param>
        /// <param name="value">比較する右辺の値を表す式ツリー。</param>
        /// <returns>比較を行う式ツリー。</returns>
        public static BinaryExpression Equals<TProperty>(Expression instance, Expression value)
            => Expression.Equal(instance, value);

        /// <summary>
        /// 左辺の値が右辺の値と異なることを評価する式ツリーを返します。
        /// </summary>
        /// <typeparam name="TProperty">左辺のプロパティの型。</typeparam>
        /// <param name="instance">左辺のプロパティへのアクセスを表す式ツリー。</param>
        /// <param name="value">比較する右辺の値を表す式ツリー。</param>
        /// <returns>比較を行う式ツリー。</returns>
        public static BinaryExpression NotEquals<TProperty>(Expression instance, Expression value)
            => Expression.NotEqual(instance, value);

        /// <summary>
        /// 左辺の値が右辺の値より大きいことを評価する式ツリーを返します。
        /// </summary>
        /// <typeparam name="TProperty">左辺のプロパティの型。</typeparam>
        /// <param name="instance">左辺のプロパティへのアクセスを表す式ツリー。</param>
        /// <param name="value">比較する右辺の値を表す式ツリー。</param>
        /// <returns>比較を行う式ツリー。</returns>
        public static BinaryExpression GreaterThan<TProperty>(Expression instance, Expression value)
        {
            var (left, right) = ConvertExpression<TProperty>(instance, value);
            return Expression.GreaterThan(left, right);
        }

        /// <summary>
        /// 左辺の値が右辺の値以上であることを評価する式ツリーを返します。
        /// </summary>
        /// <typeparam name="TProperty">左辺のプロパティの型。</typeparam>
        /// <param name="instance">左辺のプロパティへのアクセスを表す式ツリー。</param>
        /// <param name="value">比較する右辺の値を表す式ツリー。</param>
        /// <returns>比較を行う式ツリー。</returns>
        public static BinaryExpression GreaterThanOrEquals<TProperty>(Expression instance, Expression value)
        {
            var (left, right) = ConvertExpression<TProperty>(instance, value);
            return Expression.GreaterThanOrEqual(left, right);
        }

        /// <summary>
        /// 左辺の値が右辺の値より小さいことを評価する式ツリーを返します。
        /// </summary>
        /// <typeparam name="TProperty">左辺のプロパティの型。</typeparam>
        /// <param name="instance">左辺のプロパティへのアクセスを表す式ツリー。</param>
        /// <param name="value">比較する右辺の値を表す式ツリー。</param>
        /// <returns>比較を行う式ツリー。</returns>
        public static BinaryExpression LessThan<TProperty>(Expression instance, Expression value)
        {
            var (left, right) = ConvertExpression<TProperty>(instance, value);
            return Expression.LessThan(left, right);
        }

        /// <summary>
        /// 左辺の値が右辺の値以下であることを評価する式ツリーを返します。
        /// </summary>
        /// <typeparam name="TProperty">左辺のプロパティの型。</typeparam>
        /// <param name="instance">左辺のプロパティへのアクセスを表す式ツリー。</param>
        /// <param name="value">比較する右辺の値を表す式ツリー。</param>
        /// <returns>比較を行う式ツリー。</returns>
        public static BinaryExpression LessThanOrEquals<TProperty>(Expression instance, Expression value)
        {
            var (left, right) = ConvertExpression<TProperty>(instance, value);
            return Expression.LessThanOrEqual(left, right);
        }

        /// <summary>
        /// 左辺の値が右辺の値を含んでいることを評価する式ツリーを返します。
        /// </summary>
        /// <typeparam name="TProperty">左辺のプロパティの型。</typeparam>
        /// <param name="instance">左辺のプロパティへのアクセスを表す式ツリー。</param>
        /// <param name="value">比較する右辺の値を表す式ツリー。</param>
        /// <returns>比較を行う式ツリー。</returns>
        public static MethodCallExpression Contains<TProperty>(Expression instance, Expression value)
            => typeof(TProperty) == typeof(string)
             ? Expression.Call(instance, StringMetadata.ContainsMethod, value)
             : throw new InvalidOperationException();

        /// <summary>
        /// 左辺の値が右辺の値を含んでいないことを評価する式ツリーを返します。
        /// </summary>
        /// <typeparam name="TProperty">左辺のプロパティの型。</typeparam>
        /// <param name="instance">左辺のプロパティへのアクセスを表す式ツリー。</param>
        /// <param name="value">比較する右辺の値を表す式ツリー。</param>
        /// <returns>比較を行う式ツリー。</returns>
        public static UnaryExpression NotContains<TProperty>(Expression instance, Expression value)
            => Expression.Not(Contains<TProperty>(instance, value));

        /// <summary>
        /// 左辺の値の先頭が右辺の値と等しいことを評価する式ツリーを返します。
        /// </summary>
        /// <typeparam name="TProperty">左辺のプロパティの型。</typeparam>
        /// <param name="instance">左辺のプロパティへのアクセスを表す式ツリー。</param>
        /// <param name="value">比較する右辺の値を表す式ツリー。</param>
        /// <returns>比較を行う式ツリー。</returns>
        public static MethodCallExpression StartsWith<TProperty>(Expression instance, Expression value)
            => typeof(TProperty) == typeof(string)
             ? Expression.Call(instance, StringMetadata.StartsWithMethod, value)
             : throw new InvalidOperationException();

        /// <summary>
        /// 左辺の値の先頭が右辺の値と異なることを評価する式ツリーを返します。
        /// </summary>
        /// <typeparam name="TProperty">左辺のプロパティの型。</typeparam>
        /// <param name="instance">左辺のプロパティへのアクセスを表す式ツリー。</param>
        /// <param name="value">比較する右辺の値を表す式ツリー。</param>
        /// <returns>比較を行う式ツリー。</returns>
        public static UnaryExpression NotStartsWith<TProperty>(Expression instance, Expression value)
            => Expression.Not(StartsWith<TProperty>(instance, value));

        /// <summary>
        /// 左辺の値の末尾が右辺の値と等しいことを評価する式ツリーを返します。
        /// </summary>
        /// <typeparam name="TProperty">左辺のプロパティの型。</typeparam>
        /// <param name="instance">左辺のプロパティへのアクセスを表す式ツリー。</param>
        /// <param name="value">比較する右辺の値を表す式ツリー。</param>
        /// <returns>比較を行う式ツリー。</returns>
        public static MethodCallExpression EndsWith<TProperty>(Expression instance, Expression value)
            => typeof(TProperty) == typeof(string)
             ? Expression.Call(instance, StringMetadata.EndsWithMethod, value)
             : throw new InvalidOperationException();

        /// <summary>
        /// 左辺の値の末尾が右辺の値と異なることを評価する式ツリーを返します。
        /// </summary>
        /// <typeparam name="TProperty">左辺のプロパティの型。</typeparam>
        /// <param name="instance">左辺のプロパティへのアクセスを表す式ツリー。</param>
        /// <param name="value">比較する右辺の値を表す式ツリー。</param>
        /// <returns>比較を行う式ツリー。</returns>
        public static UnaryExpression NotEndsWith<TProperty>(Expression instance, Expression value)
            => Expression.Not(EndsWith<TProperty>(instance, value));

        /// <summary>
        /// 左辺の値が null であるかどうかを評価する式ツリーを返します。
        /// </summary>
        /// <typeparam name="TProperty">左辺のプロパティの型。</typeparam>
        /// <param name="instance">左辺のプロパティへのアクセスを表す式ツリー。</param>
        /// <returns>比較を行う式ツリー。</returns>
        public static BinaryExpression IsNull<TProperty>(Expression instance)
            => Expression.Equal(Expression.Convert(instance, typeof(object)), Expression.Constant(null));

        /// <summary>
        /// 値の大小比較を行うためにプロパティの型に応じて左辺と右辺の式ツリーを変換します。
        /// </summary>
        /// <typeparam name="TProperty">左辺のプロパティの型。</typeparam>
        /// <param name="instance">左辺のプロパティへのアクセスを表す式ツリー。</param>
        /// <param name="value">比較する右辺の値を表す式ツリー。</param>
        /// <returns>比較用に変換された左辺と右辺の式ツリー。</returns>
        private static (Expression left, Expression right) ConvertExpression<TProperty>(Expression instance, Expression value)
        {
            if (typeof(TProperty) == typeof(string))
            {
                var expression = Expression.Call(instance, StringMetadata.CompareToMethod, value);
                return (expression, Expression.Constant(0));
            }
            if (typeof(TProperty) == typeof(Guid))
            {
                var expression = Expression.Call(instance, GuidMetadata.CompareToMethod, value);
                return (expression, Expression.Constant(0));
            }
            if (typeof(TProperty).IsEnum)
            {
                var leftConvertExpression = Expression.Convert(instance, typeof(int));
                var rightConvertExpression = Expression.Convert(value, typeof(int));
                var expression = Expression.Call(leftConvertExpression, Int32Metadata.CompareToMethod, rightConvertExpression);
                return (expression, Expression.Constant(0));
            }
            return (instance, value);
        }
    }
}
