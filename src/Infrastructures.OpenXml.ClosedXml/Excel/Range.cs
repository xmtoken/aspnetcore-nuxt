using AspNetCoreNuxt.Extensions.OpenXml.Abstractions.Excel;
using ClosedXML.Excel;

namespace AspNetCoreNuxt.Infrastructures.OpenXml.ClosedXml.Excel
{
    /// <inheritdoc cref="IRange"/>
    internal class Range : IRange
    {
        /// <summary>
        /// <see cref="IXLRange"/> オブジェクトを取得します。
        /// </summary>
        internal IXLRange RangeInstance { get; }

        /// <summary>
        /// <see cref="Range"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="range"><see cref="IXLRange"/> オブジェクト。</param>
        public Range(IXLRange range)
        {
            RangeInstance = range;
        }

        /// <inheritdoc/>
        public ICell Cell(string address)
            => new Cell(RangeInstance.Cell(address));

        /// <inheritdoc/>
        public ICell Cell(int row, int column)
            => new Cell(RangeInstance.Cell(row, column));
    }
}
