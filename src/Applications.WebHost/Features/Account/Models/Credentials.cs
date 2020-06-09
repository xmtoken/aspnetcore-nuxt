namespace AspNetCoreNuxt.Applications.WebHost.Features.Account.Models
{
    /// <summary>
    /// 認証情報を表します。
    /// </summary>
    public class Credentials
    {
        /// <summary>
        /// ユーザー名を取得または設定します。
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// パスワードを取得または設定します。
        /// </summary>
        public string Password { get; set; }
    }
}
