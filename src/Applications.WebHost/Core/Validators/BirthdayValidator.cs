using FluentValidation.Validators;
using System;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Validators
{
    /// <summary>
    /// 生年月日の妥当性検証ロジックを提供します。
    /// </summary>
    public class BirthdayValidator : PropertyValidator
    {
        /// <summary>
        /// <see cref="BirthdayValidator"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public BirthdayValidator()
            : base("{PropertyName}は過去の日付を入力してください。")
        {
        }

        /// <summary>
        /// 値を検証します。
        /// </summary>
        /// <param name="context"><see cref="PropertyValidatorContext"/> オブジェクト。</param>
        /// <returns>検証に成功した場合は true。それ以外の場合は false。</returns>
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context.PropertyValue as DateTime?;

            if (value == null)
            {
                return true;
            }

            return value.Value < DateTime.Today;
        }
    }
}
