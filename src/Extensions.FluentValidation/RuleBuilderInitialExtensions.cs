using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Resources;

namespace AspNetCoreNuxt.Extensions.FluentValidation
{
    /// <summary>
    /// <see cref="IRuleBuilderInitial{T, TProperty}"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class RuleBuilderInitialExtensions
    {
        /// <summary>
        /// <see cref="PropertyRule.DisplayName"/> プロパティを設定します。
        /// </summary>
        /// <typeparam name="T">オブジェクトの型。</typeparam>
        /// <typeparam name="TProperty">プロパティの型。</typeparam>
        /// <param name="ruleBuilder"><see cref="IRuleBuilderInitial{T, TProperty}"/> オブジェクト。</param>
        /// <param name="name"><see cref="PropertyRule.DisplayName"/> プロパティに設定する表示名。</param>
        /// <returns><see cref="IRuleBuilderInitial{T, TProperty}"/> オブジェクト。</returns>
        public static IRuleBuilderInitial<T, TProperty> ConfigureDisplayName<T, TProperty>(this IRuleBuilderInitial<T, TProperty> ruleBuilder, string name)
            => ruleBuilder.Configure(x => x.DisplayName = new StaticStringSource(name));
    }
}
