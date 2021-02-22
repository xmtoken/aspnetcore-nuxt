using System.Text.RegularExpressions;

namespace AspNetCoreNuxt.Extensions.Identity.Specifications
{
    /// <summary>
    /// パスワードのポリシーを表します。
    /// </summary>
    public class PasswordPolicySpecification : ISpecification<string>
    {
        /// <summary>
        /// <see cref="PasswordPolicyOptions"/> オブジェクトを表します。
        /// </summary>
        private readonly PasswordPolicyOptions Options;

        /// <summary>
        /// <see cref="PasswordPolicySpecification"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="options">ポリシー条件を表す <see cref="PasswordPolicyOptions"/> オブジェクト。</param>
        public PasswordPolicySpecification(PasswordPolicyOptions options)
        {
            Options = options;
        }

        /// <inheritdoc/>
        public bool IsSatisfiedBy(string obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (Options.RequireLength > obj.Length)
            {
                return false;
            }

            if (Options.RequireDigit && !Regex.IsMatch(obj, "[0-9]+"))
            {
                return false;
            }

            if (Options.RequireLowercase && !Regex.IsMatch(obj, "[a-z]+"))
            {
                return false;
            }

            if (Options.RequireUppercase && !Regex.IsMatch(obj, "[A-Z]+"))
            {
                return false;
            }

            return true;
        }
    }
}
