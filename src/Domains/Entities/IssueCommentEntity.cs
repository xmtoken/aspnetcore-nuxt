namespace AspNetCoreNuxt.Domains.Entities
{
    /// <summary>
    /// 課題のコメントを表します。
    /// </summary>
    public class IssueCommentEntity
    {
        /// <summary>
        /// コメント ID を取得します。
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// 課題 ID を取得または設定します。
        /// </summary>
        public int IssueId { get; set; }
    }
}
