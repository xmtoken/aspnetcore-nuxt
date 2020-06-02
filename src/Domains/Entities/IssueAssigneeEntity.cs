namespace AspNetCoreNuxt.Domains.Entities
{
    /// <summary>
    /// 課題の担当者を表します。
    /// </summary>
    public class IssueAssigneeEntity
    {
        /// <summary>
        /// 課題 ID を取得または設定します。
        /// </summary>
        public int IssueId { get; set; }

        /// <summary>
        /// ユーザー ID を取得または設定します。
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 課題を取得または設定します。
        /// </summary>
        public IssueEntity Issue { get; set; }

        /// <summary>
        /// ユーザーを取得または設定します。
        /// </summary>
        public UserEntity User { get; set; }
    }
}
