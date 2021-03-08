using FluentValidation;

namespace AspNetCoreNuxt.Extensions.FluentValidation
{
    /// <summary>
    /// <see cref="IRuleBuilderInitialCollection{T, TElement}"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class RuleBuilderInitialCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">オブジェクトの型。</typeparam>
        /// <typeparam name="TElement">コレクションの要素の型。</typeparam>
        /// <param name="ruleBuilder"><see cref="IRuleBuilderInitialCollection{T, TElement}"/> オブジェクト。</param>
        /// <returns><see cref="IRuleBuilderInitialCollection{T, TElement}"/> オブジェクト。</returns>
        /// <remarks>
        /// RuleForEach(x => x).SetValidator(...); でルールの構築を行ったときに ModelState のキーが x[0].PropertyName となるため先頭の x を削除して [0].PropertyName とします。
        /// </remarks>
        public static IRuleBuilderInitialCollection<T, TElement> RemoveForEachPropertyName<T, TElement>(this IRuleBuilderInitialCollection<T, TElement> ruleBuilder)
            => ruleBuilder.Configure(x =>
            {
                x.PropertyName = "x";
                x.OnFailure = (_, validationFailures) =>
                {
                    foreach (var validationFailure in validationFailures)
                    {
                        validationFailure.PropertyName = validationFailure.PropertyName.Remove(0, x.PropertyName.Length);
                    }
                };
            });
    }
}
