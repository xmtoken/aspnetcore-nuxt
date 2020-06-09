using AspNetCoreNuxt.Domains;
using System;
using System.Collections.Generic;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Models
{
    /// <summary>
    /// ユーザーの検索条件を表します。
    /// </summary>
    public interface IUserSearchConditions
    {
        /// <summary>
        /// ユーザー名を取得します。
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// メールアドレスを取得します。
        /// </summary>
        string EmailAddress { get; }

        /// <summary>
        /// 生年月日を取得します。
        /// </summary>
        DateTime? Birthday { get; }

        /// <summary>
        /// 性別のコレクションを取得します。
        /// </summary>
        IEnumerable<Gender> Genders { get; }

        /// <summary>
        /// ロールのコレクションを取得します。
        /// </summary>
        IEnumerable<int> Roles { get; }
    }
}
