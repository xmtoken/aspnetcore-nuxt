namespace AspNetCoreNuxt.Applications.WebHost.Features.Account.Models
{
    /// <summary>
    /// 認証情報を表します。
    /// </summary>
    public class Credentials
    {
        /// <summary>
        /// ユーザー名を取得します。
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// パスワードを取得します。
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// <see cref="Credentials"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="userName">ユーザー名。</param>
        /// <param name="password">パスワード。</param>
        public Credentials(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
