using AspNetCoreNuxt.Domains;
using System;
using System.Collections.Generic;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Models
{
    /// <summary>
    /// ユーザーの登録情報を表します。
    /// </summary>
    public class UserCreateModel
    {
        /// <summary>
        /// ユーザー名を取得します。
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// パスワードを取得します。
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// メールアドレスを取得します。
        /// </summary>
        public string EmailAddress { get; private set; }

        /// <summary>
        /// 生年月日を取得します。
        /// </summary>
        public DateTime? Birthday { get; private set; }

        /// <summary>
        /// 性別を取得します。
        /// </summary>
        public Gender? Gender { get; private set; }

        /// <summary>
        /// ロールのコレクションを取得します。
        /// </summary>
        public IEnumerable<int> Roles { get; private set; }
    }
}
