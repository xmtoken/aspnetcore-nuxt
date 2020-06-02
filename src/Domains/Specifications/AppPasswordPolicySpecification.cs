using AspNetCoreNuxt.Extensions.Identity.Specifications;

namespace AspNetCoreNuxt.Domains.Specifications
{
    /// <inheritdoc/>
    public class AppPasswordPolicySpecification : PasswordPolicySpecification
    {
        /// <summary>
        /// ポリシー条件を表す <see cref="PasswordPolicyOptions"/> オブジェクトを取得します。
        /// </summary>
        public static PasswordPolicyOptions Options { get; }
            = new PasswordPolicyOptions(requireLength: 8, requireDigit: true, requireLowercase: true, requireUppercase: true);

        /// <summary>
        /// <see cref="AppPasswordPolicySpecification"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public AppPasswordPolicySpecification()
            : base(Options)
        {
        }
    }
}
