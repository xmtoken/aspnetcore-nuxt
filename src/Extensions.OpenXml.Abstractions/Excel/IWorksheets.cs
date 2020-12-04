namespace AspNetCoreNuxt.Extensions.OpenXml.Abstractions.Excel
{
    /// <summary>
    /// ワークシートのコレクションを表します。
    /// </summary>
    public interface IWorksheets
    {
        /// <summary>
        /// 指定されたインデックスのワークシートを取得します。
        /// </summary>
        /// <param name="index">ワークシートの位置を表す 1 から始まるインデックス。</param>
        /// <returns><see cref="IWorksheet"/> オブジェクト。</returns>
        public IWorksheet this[int index] { get; }

        /// <summary>
        /// 指定された名前のワークシートを取得します。
        /// </summary>
        /// <param name="name">ワークシートの名前。</param>
        /// <returns><see cref="IWorksheet"/> オブジェクト。</returns>
        public IWorksheet this[string name] { get; }

        /// <summary>
        /// 指定された名前の新しいワークシートを追加します。
        /// </summary>
        /// <param name="name">追加するワークシートの名前。</param>
        /// <returns>追加したワークシートを表す <see cref="IWorksheet"/> オブジェクト。</returns>
        IWorksheet Add(string name);
    }
}
