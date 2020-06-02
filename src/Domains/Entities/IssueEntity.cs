using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AspNetCoreNuxt.Domains.Entities
{
    /// <summary>
    /// 課題を表します。
    /// </summary>
    public class IssueEntity
    {
        /// <summary>
        /// 課題 ID を取得します。
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// 課題名を取得または設定します。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// プロジェクト ID を取得または設定します。
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// ワークフロー ID を取得または設定します。
        /// </summary>
        public int WorkflowId { get; set; }

        /// <summary>
        /// プロジェクトを取得または設定します。
        /// </summary>
        public ProjectEntity Project { get; set; }

        /// <summary>
        /// ワークフローを取得または設定します。
        /// </summary>
        public IssueWorkflowEntity Workflow { get; set; }

        /// <summary>
        /// 担当者のコレクションを取得または設定します。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:コレクション プロパティは読み取り専用でなければなりません")]
        public ICollection<IssueAssigneeEntity> Assignees { get; set; }

        /// <summary>
        /// コメントのコレクションを取得または設定します。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:コレクション プロパティは読み取り専用でなければなりません")]
        public ICollection<IssueCommentEntity> Comments { get; set; }
    }
}
