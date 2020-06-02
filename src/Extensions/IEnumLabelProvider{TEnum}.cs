using System;

namespace AspNetCoreNuxt.Extensions
{
    /// <summary>
    /// <see cref="Enum"/> クラスの値に対応するラベルテキストを提供するプロバイダーを表します。
    /// </summary>
    /// <typeparam name="TEnum">列挙体の型。</typeparam>
    public interface IEnumLabelProvider<TEnum> : IEnumLabelProvider
        where TEnum : Enum
    {
        /// <summary>
        /// ラベルテキストを取得します。
        /// </summary>
        /// <param name="value">列挙体の値。</param>
        /// <returns>ラベルテキスト。</returns>
        string CreateLabel(TEnum value);
    }
}
