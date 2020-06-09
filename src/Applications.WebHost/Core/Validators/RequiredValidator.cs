using FluentValidation.Validators;
using System.Collections;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Validators
{
    /// <summary>
    /// 生年月日の妥当性検証ロジックを提供します。
    /// </summary>
    public class RequiredValidator : PropertyValidator
    {
        /// <summary>
        /// <see cref="RequiredValidator"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public RequiredValidator()
            : base("{PropertyName}を入力してください。")
        {
        }

        /// <summary>
        /// 値を検証します。
        /// </summary>
        /// <param name="context"><see cref="PropertyValidatorContext"/> オブジェクト。</param>
        /// <returns>検証に成功した場合は true。それ以外の場合は false。</returns>
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context.PropertyValue as IEnumerable;
            return value != null && value.GetEnumerator().MoveNext();
        }
    }
}
