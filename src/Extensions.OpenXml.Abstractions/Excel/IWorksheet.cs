namespace AspNetCoreNuxt.Extensions.OpenXml.Abstractions.Excel
{
    /// <summary>
    /// ワークシートを表します。
    /// </summary>
    public interface IWorksheet
    {
        /// <summary>
        /// 指定されたアドレスのセルを取得します。
        /// </summary>
        /// <param name="address">セルのアドレス。</param>
        /// <returns><see cref="ICell"/> オブジェクト。</returns>
        ICell Cell(string address);

        /// <summary>
        /// 指定された行と列のセルを取得します。
        /// </summary>
        /// <param name="row">行の位置を表す 1 から始まるインデックス。</param>
        /// <param name="column">列の位置を表す 1 から始まるインデックス。</param>
        /// <returns><see cref="ICell"/> オブジェクト。</returns>
        ICell Cell(int row, int column);

        /// <summary>
        /// 指定されたアドレスのセル範囲を取得します。
        /// </summary>
        /// <param name="address">セル範囲のアドレス。</param>
        /// <returns><see cref="IRange"/> オブジェクト。</returns>
        IRange Range(string address);
    }
}
