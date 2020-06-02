using System;

namespace AspNetCoreNuxt.Extensions
{
    /// <summary>
    /// <see cref="IEnumLabelProvider"/> オブジェクトを作成する機能を提供します。
    /// </summary>
    public interface IEnumLabelProviderFactory
    {
        /// <summary>
        /// <see cref="IEnumLabelProvider"/> オブジェクトの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="type">列挙体の型。</param>
        /// <returns><see cref="IEnumLabelProvider"/> オブジェクト。</returns>
        IEnumLabelProvider CreateProvider(Type type);
    }
}
