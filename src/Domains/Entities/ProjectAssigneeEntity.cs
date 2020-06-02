namespace AspNetCoreNuxt.Domains.Entities
{
    /// <summary>
    /// プロジェクトの担当者を表します。
    /// </summary>
    public class ProjectAssigneeEntity
    {
        /// <summary>
        /// プロジェクト ID を取得または設定します。
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// ユーザー ID を取得または設定します。
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// プロジェクトを取得または設定します。
        /// </summary>
        public ProjectEntity Project { get; set; }

        /// <summary>
        /// ユーザーを取得または設定します。
        /// </summary>
        public UserEntity User { get; set; }
    }
}
