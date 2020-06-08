using FluentValidation;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Validators
{
    /// <summary>
    /// <see cref="IRuleBuilder{T, TProperty}"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class RuleBuilderExtensions
    {
        /// <summary>
        /// <see cref="BirthdayValidator"/> クラスで定義された検証ロジックを適用します。
        /// </summary>
        /// <typeparam name="T">オブジェクトの型。</typeparam>
        /// <typeparam name="TProperty">プロパティの型。</typeparam>
        /// <param name="ruleBuilder"><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</param>
        /// <returns><see cref="IRuleBuilderOptions{T, TProperty}"/> オブジェクト。</returns>
        public static IRuleBuilderOptions<T, TProperty> Birthday<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
            => ruleBuilder.SetValidator(new BirthdayValidator());

        /// <summary>
        /// <see cref="PasswordValidator"/> クラスで定義された検証ロジックを適用します。
        /// </summary>
        /// <typeparam name="T">オブジェクトの型。</typeparam>
        /// <typeparam name="TProperty">プロパティの型。</typeparam>
        /// <param name="ruleBuilder"><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</param>
        /// <returns><see cref="IRuleBuilderOptions{T, TProperty}"/> オブジェクト。</returns>
        public static IRuleBuilderOptions<T, TProperty> Password<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
            => ruleBuilder.SetValidator(new PasswordValidator());

        /// <summary>
        /// <see cref="PostalCodeValidator"/> クラスで定義された検証ロジックを適用します。
        /// </summary>
        /// <typeparam name="T">オブジェクトの型。</typeparam>
        /// <typeparam name="TProperty">プロパティの型。</typeparam>
        /// <param name="ruleBuilder"><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</param>
        /// <returns><see cref="IRuleBuilderOptions{T, TProperty}"/> オブジェクト。</returns>
        public static IRuleBuilderOptions<T, TProperty> PostalCode<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
            => ruleBuilder.SetValidator(new PostalCodeValidator());

        /// <summary>
        /// <see cref="UserNameValidator"/> クラスで定義された検証ロジックを適用します。
        /// </summary>
        /// <typeparam name="T">オブジェクトの型。</typeparam>
        /// <typeparam name="TProperty">プロパティの型。</typeparam>
        /// <param name="ruleBuilder"><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</param>
        /// <returns><see cref="IRuleBuilderOptions{T, TProperty}"/> オブジェクト。</returns>
        public static IRuleBuilderOptions<T, TProperty> UserName<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
            => ruleBuilder.SetValidator(new UserNameValidator());
    }
}
