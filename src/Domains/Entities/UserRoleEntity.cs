namespace AspNetCoreNuxt.Domains.Entities
{
    /// <summary>
    /// ユーザーのロールを表します。
    /// </summary>
    public class UserRoleEntity
    {
        /// <summary>
        /// ユーザー ID を取得または設定します。
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// ロール ID を取得または設定します。
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// ロールを取得または設定します。
        /// </summary>
        public RoleEntity Role { get; set; }
    }
}
