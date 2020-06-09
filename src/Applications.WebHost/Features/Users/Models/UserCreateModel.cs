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
        /// ユーザー名を取得または設定します。
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// パスワードを取得または設定します。
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// メールアドレスを取得または設定します。
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// 生年月日を取得または設定します。
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 性別を取得または設定します。
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// ロールのコレクションを取得または設定します。
        /// </summary>
        public IEnumerable<int> Roles { get; set; }
    }
}
