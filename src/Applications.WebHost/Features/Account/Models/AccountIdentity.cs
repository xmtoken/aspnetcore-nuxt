using AspNetCoreNuxt.Domains;
using System.Collections.Generic;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Account.Models
{
    /// <summary>
    /// アカウントの情報を表します。
    /// </summary>
    public class AccountIdentity
    {
        /// <summary>
        /// ユーザー名を取得します。
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// 権限を表す <see cref="Permission"/> 値をキーとしたコレクションを取得します。
        /// </summary>
        public IDictionary<string, bool> Permissions { get; }

        /// <summary>
        /// <see cref="AccountIdentity"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="userName">ユーザー名。</param>
        /// <param name="permissions">権限を表す <see cref="Permission"/> 値をキーとしたコレクション。</param>
        public AccountIdentity(string userName, IDictionary<string, bool> permissions)
        {
            UserName = userName;
            Permissions = permissions;
        }
    }
}
