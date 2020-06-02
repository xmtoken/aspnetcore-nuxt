using AspNetCoreNuxt.Extensions.Identity.Specifications;

namespace AspNetCoreNuxt.Domains.Specifications
{
    /// <inheritdoc/>
    public class AppUserNamePolicySpecification : UserNamePolicySpecification
    {
        /// <summary>
        /// ポリシー条件を表す <see cref="UserNamePolicyOptions"/> オブジェクトを取得します。
        /// </summary>
        public static UserNamePolicyOptions Options { get; }
            = new UserNamePolicyOptions(requireLength: 8, requireDigit: true, requireLowercase: true, requireUppercase: true);

        /// <summary>
        /// <see cref="AppUserNamePolicySpecification"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public AppUserNamePolicySpecification()
            : base(Options)
        {
        }
    }
}
