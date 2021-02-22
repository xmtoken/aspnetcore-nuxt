using System.Text.RegularExpressions;

namespace AspNetCoreNuxt.Extensions.Identity.Specifications
{
    /// <summary>
    /// ユーザー名のポリシーを表します。
    /// </summary>
    public class UserNamePolicySpecification : ISpecification<string>
    {
        /// <summary>
        /// <see cref="UserNamePolicyOptions"/> オブジェクトを表します。
        /// </summary>
        private readonly UserNamePolicyOptions Options;

        /// <summary>
        /// <see cref="UserNamePolicySpecification"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="options">ポリシー条件を表す <see cref="UserNamePolicyOptions"/> オブジェクト。</param>
        public UserNamePolicySpecification(UserNamePolicyOptions options)
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
