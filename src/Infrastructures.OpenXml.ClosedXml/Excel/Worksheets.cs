using AspNetCoreNuxt.Extensions.OpenXml.Abstractions.Excel;
using ClosedXML.Excel;

namespace AspNetCoreNuxt.Infrastructures.OpenXml.ClosedXml.Excel
{
    /// <inheritdoc cref="IWorksheets"/>
    internal class Worksheets : IWorksheets
    {
        /// <summary>
        /// <see cref="IXLWorksheets"/> オブジェクトを表します。
        /// </summary>
        private readonly IXLWorksheets WorksheetsInstance;

        /// <summary>
        /// <see cref="Worksheets"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="worksheets"><see cref="IXLWorksheets"/> オブジェクト。</param>
        public Worksheets(IXLWorksheets worksheets)
        {
            WorksheetsInstance = worksheets;
        }

        /// <inheritdoc/>
        public IWorksheet this[int index]
            => new Worksheet(WorksheetsInstance.Worksheet(index));

        /// <inheritdoc/>
        public IWorksheet this[string name]
            => new Worksheet(WorksheetsInstance.Worksheet(name));

        /// <inheritdoc/>
        public IWorksheet Add(string name)
            => new Worksheet(WorksheetsInstance.Add(name));
    }
}
