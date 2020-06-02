namespace AspNetCoreNuxt.Domains.Entities
{
    /// <summary>
    /// 課題のワークフローを表します。
    /// </summary>
    public class IssueWorkflowEntity
    {
        /// <summary>
        /// ワークフロー ID を取得します。
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// ワークフロー名を取得または設定します。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// RGBA 値 (#RRGGBBAA) を取得または設定します。
        /// </summary>
        public string Color { get; set; }
    }
}
