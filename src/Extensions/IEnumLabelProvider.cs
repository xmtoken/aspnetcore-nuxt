using System;

namespace AspNetCoreNuxt.Extensions
{
    /// <summary>
    /// <see cref="Enum"/> クラスの値に対応するラベルテキストを提供するプロバイダーを表します。
    /// </summary>
    public interface IEnumLabelProvider
    {
        /// <summary>
        /// ラベルテキストを取得します。
        /// </summary>
        /// <param name="value">列挙体の値。</param>
        /// <returns>ラベルテキスト。</returns>
        string CreateLabel(object value);
    }
}
