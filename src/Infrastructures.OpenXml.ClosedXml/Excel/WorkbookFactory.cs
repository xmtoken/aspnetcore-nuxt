using AspNetCoreNuxt.Extensions.OpenXml.Abstractions.Excel;
using ClosedXML.Excel;
using System.IO;

namespace AspNetCoreNuxt.Infrastructures.OpenXml.ClosedXml.Excel
{
    /// <inheritdoc cref="IWorkbookFactory"/>
    public class WorkbookFactory : IWorkbookFactory
    {
        /// <inheritdoc/>
        public IWorkbook Create()
               => new Workbook(new XLWorkbook());

        /// <inheritdoc/>
        public IWorkbook Create(Stream stream)
               => new Workbook(new XLWorkbook(stream));
    }
}
