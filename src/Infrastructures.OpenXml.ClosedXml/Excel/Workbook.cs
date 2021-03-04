using AspNetCoreNuxt.Extensions.OpenXml.Abstractions.Excel;
using ClosedXML.Excel;
using System;
using System.IO;

namespace AspNetCoreNuxt.Infrastructures.OpenXml.ClosedXml.Excel
{
    /// <inheritdoc cref="IWorkbook"/>
    internal class Workbook : IWorkbook
    {
        /// <summary>
        /// リソースを開放済みかどうかを表します。
        /// </summary>
        private bool Disposed;

        /// <summary>
        /// <see cref="IXLWorkbook"/> オブジェクトを表します。
        /// </summary>
        private readonly IXLWorkbook WorkbookInstance;

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
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="IDisposable.Dispose()"/>
        /// <param name="disposing">マネージドリソースを開放する場合は true。それ以外の場合は false。</param>
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
