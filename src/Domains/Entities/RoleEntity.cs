using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AspNetCoreNuxt.Domains.Entities
{
    /// <summary>
    /// ロールを表します。
    /// </summary>
    public class RoleEntity
    {
        /// <summary>
        /// ロール ID を取得します。
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// ロール名を取得または設定します。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 権限のコレクションを取得または設定します。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:コレクション プロパティは読み取り専用でなければなりません")]
        public ICollection<RolePermissionEntity> Permissions { get; set; }
    }
}
