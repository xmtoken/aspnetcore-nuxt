using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AspNetCoreNuxt.Domains.Entities
{
    /// <summary>
    /// プロジェクトを表します。
    /// </summary>
    public class ProjectEntity
    {
        /// <summary>
        /// プロジェクト ID を取得します。
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// プロジェクト名を取得または設定します。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 担当者のコレクションを取得または設定します。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:コレクション プロパティは読み取り専用でなければなりません")]
        public ICollection<ProjectAssigneeEntity> Assignees { get; set; }

        /// <summary>
        /// 課題のコレクションを取得または設定します。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:コレクション プロパティは読み取り専用でなければなりません")]
        public ICollection<IssueEntity> Issues { get; set; }
    }
}
