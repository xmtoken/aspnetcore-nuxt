using AspNetCoreNuxt.Extensions.OpenXml.Abstractions.Excel;
using ClosedXML.Excel;

namespace AspNetCoreNuxt.Infrastructures.OpenXml.ClosedXml.Excel
{
    /// <inheritdoc cref="ICell"/>
    internal class Cell : ICell
    {
        /// <summary>
        /// <see cref="IXLCell"/> オブジェクトを表します。
        /// </summary>
        private readonly IXLCell CellInstance;

        /// <inheritdoc/>
        public object Value
        {
            get => CellInstance.Value;
            set => CellInstance.Value = value;
        }

        /// <summary>
        /// <see cref="Cell"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="cell"><see cref="IXLCell"/> オブジェクト。</param>
        public Cell(IXLCell cell)
        {
            CellInstance = cell;
        }
    }
}
