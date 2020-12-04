using AspNetCoreNuxt.Extensions.OpenXml.Abstractions.Excel;
using ClosedXML.Excel;

namespace AspNetCoreNuxt.Infrastructures.OpenXml.ClosedXml.Excel
{
    /// <inheritdoc cref="IWorksheet"/>
    internal class Worksheet : IWorksheet
    {
        /// <summary>
        /// <see cref="IXLWorksheet"/> オブジェクトを取得します。
        /// </summary>
        internal IXLWorksheet WorksheetInstance { get; }

        /// <summary>
        /// <see cref="Worksheet"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="worksheet"><see cref="IXLWorksheet"/> オブジェクト。</param>
        public Worksheet(IXLWorksheet worksheet)
        {
            WorksheetInstance = worksheet;
        }

        /// <inheritdoc/>
        public ICell Cell(string address)
            => new Cell(WorksheetInstance.Cell(address));

        /// <inheritdoc/>
        public ICell Cell(int row, int column)
            => new Cell(WorksheetInstance.Cell(row, column));

        /// <inheritdoc/>
        public IRange Range(string address)
            => new Range(WorksheetInstance.Range(address));
    }
}
