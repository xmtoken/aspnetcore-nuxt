using System;

namespace AspNetCoreNuxt.Extensions.OpenXml.Abstractions.Excel
{
    /// <summary>
    /// ワークブックを表します。
    /// </summary>
    public interface IWorkbook : IDisposable
    {
        /// <summary>
        /// ワークシートのコレクションを取得します。
        /// </summary>
        IWorksheets Worksheets { get; }

        /// <summary>
        /// ワークブックを表すバイト配列を返します。
        /// </summary>
        /// <returns>ワークブックを表すバイト配列。</returns>
        byte[] ToByteArray();
    }
}
