using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AspNetCoreNuxt.Domains.Entities
{
    /// <summary>
    /// ユーザーを表します。
    /// </summary>
    public class UserEntity
    {
        /// <summary>
        /// ユーザー ID を取得します。
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// ユーザー名を取得または設定します。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 正規化されたユーザー名を取得または設定します。
        /// </summary>
        public string NormalizedName { get; set; }

        /// <summary>
        /// パスワードハッシュ値を取得または設定します。
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// プロフィールを取得または設定します。
        /// </summary>
        public UserProfileEntity Profile { get; set; }

        /// <summary>
        /// ロールのコレクションを取得または設定します。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:コレクション プロパティは読み取り専用でなければなりません")]
        public ICollection<UserRoleEntity> Roles { get; set; }

        /// <summary>
        /// 担当するプロジェクトのコレクションを取得または設定します。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:コレクション プロパティは読み取り専用でなければなりません")]
        public ICollection<ProjectAssigneeEntity> AssignedProjects { get; set; }

        /// <summary>
        /// 担当する課題のコレクションを取得または設定します。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:コレクション プロパティは読み取り専用でなければなりません")]
        public ICollection<IssueAssigneeEntity> AssignedIssues { get; set; }
    }
}
