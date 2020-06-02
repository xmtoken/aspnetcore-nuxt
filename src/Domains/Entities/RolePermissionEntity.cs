namespace AspNetCoreNuxt.Domains.Entities
{
    /// <summary>
    /// ロールの権限を表します。
    /// </summary>
    public class RolePermissionEntity
    {
        /// <summary>
        /// ロール ID を取得または設定します。
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 権限を表す <see cref="Permission"/> 値を取得または設定します。
        /// </summary>
        public Permission PermissionId { get; set; }
    }
}
