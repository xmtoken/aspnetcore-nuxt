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
        public string UserName { get; private set; }

        /// <summary>
        /// パスワードを取得します。
        /// </summary>
        public string Password { get; private set; }
    }
}
