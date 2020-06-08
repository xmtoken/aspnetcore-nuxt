using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Validators
{
    /// <summary>
    /// 郵便番号の妥当性検証ロジックを提供します。
    /// </summary>
    public class PostalCodeValidator : PropertyValidator
    {
        /// <summary>
        /// <see cref="PostalCodeValidator"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public PostalCodeValidator()
            : base("{PropertyName}はハイフン区切りの数字7文字で入力してください。")
        {
        }

        /// <summary>
        /// 値を検証します。
        /// </summary>
        /// <param name="context"><see cref="PropertyValidatorContext"/> オブジェクト。</param>
        /// <returns>検証に成功した場合は true。それ以外の場合は false。</returns>
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context.PropertyValue as string;
            return value == null || Regex.IsMatch(value, "[0-9]{3}-[0-9]{4}");
        }
    }
}
