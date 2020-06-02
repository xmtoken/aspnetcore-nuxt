using AspNetCoreNuxt.Domains.Entities;
using System.Collections.Generic;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Entities
{
    /// <summary>
    /// ユーザーを表します。
    /// </summary>
    public class User : IEntity<UserEntity>
    {
        /// <summary>
        /// ユーザー ID を取得します。
        /// </summary>
        public int? Id { get; private set; }

        /// <summary>
        /// ユーザー名を取得します。
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// プロフィールを取得します。
        /// </summary>
        public UserProfile Profile { get; private set; }

        /// <summary>
        /// ロールのコレクションを取得します。
        /// </summary>
        public IEnumerable<UserRole> Roles { get; private set; }
    }
}
