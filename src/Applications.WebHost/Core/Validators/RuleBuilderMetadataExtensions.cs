using AspNetCoreNuxt.Extensions;
using AspNetCoreNuxt.Extensions.EntityFrameworkCore.Metadata;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Validators
{
    /// <summary>
    /// <see cref="IRuleBuilder{T, TProperty}"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class RuleBuilderMetadataExtensions
    {
        /// <summary>
        /// プロパティのメタデータをもとに検証ロジックを適用します。
        /// </summary>
        /// <typeparam name="T">オブジェクトの型。</typeparam>
        /// <param name="ruleBuilder"><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</param>
        /// <param name="metadata"><see cref="IEntityMetadata"/> オブジェクト。</param>
        /// <returns><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</returns>
        public static IRuleBuilder<T, string> SetValidatorByMetadata<T>(this IRuleBuilder<T, string> ruleBuilder, IEntityMetadata metadata)
        {
            var property = ruleBuilder.GetProperty(metadata);
            return SetValidatorByMetadata(ruleBuilder, property);
        }

        /// <summary>
        /// プロパティのメタデータをもとに検証ロジックを適用します。
        /// </summary>
        /// <typeparam name="T">オブジェクトの型。</typeparam>
        /// <param name="ruleBuilder"><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</param>
        /// <param name="property"><see cref="IProperty"/> オブジェクト。</param>
        /// <returns><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</returns>
        public static IRuleBuilder<T, string> SetValidatorByMetadata<T>(this IRuleBuilder<T, string> ruleBuilder, IProperty property)
            => ruleBuilder.SetNotNullValidator(property)
                          .SetIsInEnumValidator()
                          .SetMaximumLengthValidator(property)
                          .SetScalePrecisionValidator(property);

        /// <summary>
        /// プロパティのメタデータをもとに検証ロジックを適用します。
        /// </summary>
        /// <typeparam name="T">オブジェクトの型。</typeparam>
        /// <typeparam name="TProperty">プロパティの型。</typeparam>
        /// <param name="ruleBuilder"><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</param>
        /// <param name="metadata"><see cref="IEntityMetadata"/> オブジェクト。</param>
        /// <returns><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</returns>
        public static IRuleBuilder<T, TProperty> SetValidatorByMetadata<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, IEntityMetadata metadata)
        {
            var property = ruleBuilder.GetProperty(metadata);
            return SetValidatorByMetadata(ruleBuilder, property);
        }

        /// <summary>
        /// プロパティのメタデータをもとに検証ロジックを適用します。
        /// </summary>
        /// <typeparam name="T">オブジェクトの型。</typeparam>
        /// <typeparam name="TProperty">プロパティの型。</typeparam>
        /// <param name="ruleBuilder"><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</param>
        /// <param name="property"><see cref="IProperty"/> オブジェクト。</param>
        /// <returns><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</returns>
        public static IRuleBuilder<T, TProperty> SetValidatorByMetadata<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, IProperty property)
            => ruleBuilder.SetNotNullValidator(property)
                          .SetIsInEnumValidator()
                          .SetScalePrecisionValidator(property);

        /// <summary>
        /// 検証プロパティのメタデータを取得します。
        /// </summary>
        /// <typeparam name="T">オブジェクトの型。</typeparam>
        /// <typeparam name="TProperty">プロパティの型。</typeparam>
        /// <param name="ruleBuilder"><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</param>
        /// <param name="metadata"><see cref="IEntityMetadata"/> オブジェクト。</param>
        /// <returns><see cref="IProperty"/> オブジェクト。</returns>
        private static IProperty GetProperty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, IEntityMetadata metadata)
        {
            var internalRuleBuilder = (RuleBuilder<T, TProperty>)ruleBuilder;
            return metadata.FindProperty(internalRuleBuilder.Rule.Expression);
        }

        /// <summary>
        /// プロパティのメタデータをもとに必要に応じて <see cref="EnumValidator"/> クラスの検証ロジックを適用します。
        /// </summary>
        /// <typeparam name="T">オブジェクトの型。</typeparam>
        /// <typeparam name="TProperty">プロパティの型。</typeparam>
        /// <param name="ruleBuilder"><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</param>
        /// <returns><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</returns>
        private static IRuleBuilder<T, TProperty> SetIsInEnumValidator<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            var propertyType = typeof(TProperty);
            if (propertyType.IsEnum || (propertyType.IsNullable() && propertyType.GenericTypeArguments[0].IsEnum))
            {
                return ruleBuilder.IsInEnum();
            }
            return ruleBuilder;
        }

        /// <summary>
        /// プロパティのメタデータをもとに必要に応じて <see cref="MaximumLengthValidator"/> クラスの検証ロジックを適用します。
        /// </summary>
        /// <typeparam name="T">オブジェクトの型。</typeparam>
        /// <param name="ruleBuilder"><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</param>
        /// <param name="property"><see cref="IProperty"/> オブジェクト。</param>
        /// <returns><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</returns>
        private static IRuleBuilder<T, string> SetMaximumLengthValidator<T>(this IRuleBuilder<T, string> ruleBuilder, IProperty property)
            => property.GetMaxLength() is int maximum ? ruleBuilder.MaximumLength(maximum) : ruleBuilder;

        /// <summary>
        /// プロパティのメタデータをもとに必要に応じて <see cref="NotNullValidator"/> クラスの検証ロジックを適用します。
        /// </summary>
        /// <typeparam name="T">オブジェクトの型。</typeparam>
        /// <typeparam name="TProperty">プロパティの型。</typeparam>
        /// <param name="ruleBuilder"><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</param>
        /// <param name="property"><see cref="IProperty"/> オブジェクト。</param>
        /// <returns><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</returns>
        private static IRuleBuilder<T, TProperty> SetNotNullValidator<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, IProperty property)
            => property.IsNullable ? ruleBuilder : ruleBuilder.NotNull();

        /// <summary>
        /// プロパティのメタデータをもとに必要に応じて <see cref="ScalePrecisionValidator"/> クラスの検証ロジックを適用します。
        /// </summary>
        /// <typeparam name="T">オブジェクトの型。</typeparam>
        /// <typeparam name="TProperty">プロパティの型。</typeparam>
        /// <param name="ruleBuilder"><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</param>
        /// <param name="property"><see cref="IProperty"/> オブジェクト。</param>
        /// <returns><see cref="IRuleBuilder{T, TProperty}"/> オブジェクト。</returns>
        private static IRuleBuilder<T, TProperty> SetScalePrecisionValidator<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, IProperty property)
        {
            var precision = property.GetPrecision();
            var scale = property.GetScale();
            if (precision.HasValue)
            {
                //return ruleBuilder.ScalePrecision(scale ?? 0, precision.Value);
            }
            return ruleBuilder;
        }
    }
}
