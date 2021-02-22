using AspNetCoreNuxt.Extensions.OpenXml.Abstractions.Excel;
using ClosedXML.Excel;
using System;
using System.IO;

namespace AspNetCoreNuxt.Infrastructures.OpenXml.ClosedXml.Excel
{
    /// <inheritdoc cref="IWorkbook"/>
    internal class Workbook : IWorkbook
    {
        private bool Disposed;

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

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
            {
                return;
            }
            if (disposing)
            {
                WorkbookInstance.Dispose();
            }
            Disposed = true;
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
