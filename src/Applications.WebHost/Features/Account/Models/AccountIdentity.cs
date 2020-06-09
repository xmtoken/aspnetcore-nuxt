using AspNetCoreNuxt.Domains;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Account.Models
{
    /// <summary>
    /// アカウントの情報を表します。
    /// </summary>
    public class AccountIdentity
    {
        /// <summary>
        /// ユーザー名を取得または設定します。
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 権限を表す <see cref="Permission"/> 値をキーとしたコレクションを取得または設定します。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:コレクション プロパティは読み取り専用でなければなりません")]
        public IDictionary<string, bool> Permissions { get; set; }
    }
}
