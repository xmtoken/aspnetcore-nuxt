using AspNetCoreNuxt.Extensions.OpenXml.Abstractions.Excel;
using ClosedXML.Excel;
using System.IO;

namespace AspNetCoreNuxt.Infrastructures.OpenXml.ClosedXml.Excel
{
    /// <inheritdoc cref="IWorkbook"/>
    internal class Workbook : IWorkbook
    {
        /// <summary>
        /// <see cref="IXLWorkbook"/> オブジェクトを取得します。
        /// </summary>
        internal IXLWorkbook WorkbookInstance { get; }

        /// <inheritdoc/>
        public IWorksheets Worksheets { get; }

        /// <summary>
        /// <see cref="Workbook"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="workbook"><see cref="IXLWorkbook"/> オブジェクト。</param>
        public Workbook(IXLWorkbook workbook)
        {
            WorkbookInstance = workbook;
            Worksheets = new Worksheets(WorkbookInstance.Worksheets);
        }

        /// <inheritdoc/>
        public byte[] ToByteArray()
        {
            using var memory = new MemoryStream();
            WorkbookInstance.SaveAs(memory);
            return memory.ToArray();
        }
    }
}
