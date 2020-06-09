using AspNetCoreNuxt.Domains;
using System;
using System.Collections.Generic;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Models
{
    /// <summary>
    /// ユーザーの更新情報を表します。
    /// </summary>
    public class UserUpdateModel
    {
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
