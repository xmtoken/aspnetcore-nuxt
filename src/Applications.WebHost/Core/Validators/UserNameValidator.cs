using AspNetCoreNuxt.Domains.Specifications;
using FluentValidation.Validators;
using System.Linq;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Validators
{
    /// <summary>
    /// ユーザー名のポリシーの検証ロジックを提供します。
    /// </summary>
    public class UserNameValidator : PropertyValidator
    {
        /// <summary>
        /// エラーメッセージを取得します。
        /// </summary>
        private static string ErrorMessage
        {
            get
            {
                var options = AppUserNamePolicySpecification.Options;

                var message = "{PropertyName}は";

                if (options.RequireDigit || options.RequireLowercase || options.RequireUppercase)
                {
                    var symbols = new[]
                    {
                         options.RequireDigit ? "数字" : null,
                         options.RequireLowercase ? "英小文字" : null,
                         options.RequireUppercase ? "英大文字" : null,
                    };
                    message += $"{string.Join('、', symbols.Where(x => x != null))}を含めた";
                }

                return $"{message}{options.RequireLength}文字以上で入力してください。";
            }
        }

        /// <summary>
        /// <see cref="AppUserNamePolicySpecification"/> オブジェクトを表します。
        /// </summary>
        private static readonly AppUserNamePolicySpecification Specification = new AppUserNamePolicySpecification();

        /// <summary>
        /// <see cref="UserNameValidator"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public UserNameValidator()
            : base(ErrorMessage)
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
            return value == null || Specification.IsSatisfiedBy(value);
        }
    }
}
