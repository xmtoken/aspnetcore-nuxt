using System.IO;

namespace AspNetCoreNuxt.Extensions.OpenXml.Abstractions.Excel
{
    /// <summary>
    /// <see cref="IWorkbook"/> オブジェクトを作成する機能を提供します。
    /// </summary>
    public interface IWorkbookFactory
    {
        /// <summary>
        /// <see cref="IWorkbook"/> オブジェクトの新しいインスタンスを作成します。
        /// </summary>
        /// <returns><see cref="IWorkbook"/> オブジェクト。</returns>
        public IWorkbook Create();

        /// <summary>
        /// <see cref="IWorkbook"/> オブジェクトの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> オブジェクト。</param>
        /// <returns><see cref="IWorkbook"/> オブジェクト。</returns>
        public IWorkbook Create(Stream stream);
    }
}
