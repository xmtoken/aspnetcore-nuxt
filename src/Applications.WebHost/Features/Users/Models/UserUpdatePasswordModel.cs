namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Models
{
    /// <summary>
    /// ユーザーのパスワードの更新情報を表します。
    /// </summary>
    public class UserUpdatePasswordModel
    {
        /// <summary>
        /// 現在のパスワードを取得します。
        /// </summary>
        public string CurrentPassword { get; private set; }

        /// <summary>
        /// 新しいパスワードを取得します。
        /// </summary>
        public string NewPassword { get; private set; }
    }
}
