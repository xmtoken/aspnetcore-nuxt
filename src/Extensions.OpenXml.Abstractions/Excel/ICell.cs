namespace AspNetCoreNuxt.Extensions.OpenXml.Abstractions.Excel
{
    /// <summary>
    /// セルを表します。
    /// </summary>
    public interface ICell
    {
        /// <summary>
        /// セルの値を取得または設定します。
        /// </summary>
        object Value { get; set; }
    }
}
