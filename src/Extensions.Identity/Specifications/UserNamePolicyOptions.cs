namespace AspNetCoreNuxt.Extensions.Identity.Specifications
{
    /// <summary>
    /// ユーザー名のポリシー条件を表します。
    /// </summary>
    public class UserNamePolicyOptions
    {
        /// <summary>
        /// 数値を含める必要があるかどうかを取得します。
        /// </summary>
        public bool RequireDigit { get; }

        /// <summary>
        /// 必要とする最小の桁数を取得します。
        /// </summary>
        public int RequireLength { get; }

        /// <summary>
        /// 英小文字を含める必要があるかどうかを取得します。
        /// </summary>
        public bool RequireLowercase { get; }

        /// <summary>
        /// 英大文字を含める必要があるかどうかを取得します。
        /// </summary>
        public bool RequireUppercase { get; }

        /// <summary>
        /// <see cref="UserNamePolicyOptions"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="requireLength">必要とする最小の桁数。</param>
        /// <param name="requireDigit">数値を含める必要がある場合は true。それ以外の場合は false。</param>
        /// <param name="requireLowercase">英小文字を含める必要がある場合は true。それ以外の場合は false。</param>
        /// <param name="requireUppercase">英大文字を含める必要がある場合は true。それ以外の場合は false。</param>
        public UserNamePolicyOptions(int requireLength, bool requireDigit, bool requireLowercase, bool requireUppercase)
        {
            RequireLength = requireLength;
            RequireDigit = requireDigit;
            RequireLowercase = requireLowercase;
            RequireUppercase = requireUppercase;
        }
    }
}
