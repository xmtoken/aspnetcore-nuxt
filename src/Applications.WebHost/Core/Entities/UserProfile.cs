using AspNetCoreNuxt.Domains;
using AspNetCoreNuxt.Domains.Entities;
using System;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Entities
{
    /// <summary>
    /// ユーザーのプロフィールを表します。
    /// </summary>
    public class UserProfile : IEntity<UserProfileEntity>
    {
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
    }
}
