using AspNetCoreNuxt.Domains.Entities;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Entities
{
    /// <summary>
    /// ユーザーのロールを表します。
    /// </summary>
    public class UserRole : IEntity<UserRoleEntity>
    {
        /// <summary>
        /// ロール ID を取得します。
        /// </summary>
        public int? RoleId { get; private set; }
    }
}
