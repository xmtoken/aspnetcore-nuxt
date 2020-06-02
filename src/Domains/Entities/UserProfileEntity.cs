using System;

namespace AspNetCoreNuxt.Domains.Entities
{
    /// <summary>
    /// ユーザーのプロフィールを表します。
    /// </summary>
    public class UserProfileEntity
    {
        /// <summary>
        /// プロフィール ID を取得します。
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// ユーザー ID を取得または設定します。
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// メールアドレスを取得または設定します。
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// 正規化されたメールアドレスを取得または設定します。
        /// </summary>
        public string NormalizedEmailAddress { get; set; }

        /// <summary>
        /// 生年月日を取得または設定します。
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 性別を取得または設定します。
        /// </summary>
        public Gender Gender { get; set; }
    }
}
