using System;

namespace AspNetCoreNuxt.Extensions
{
    /// <summary>
    /// <see cref="IEnumLabelProviderFactory"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class EnumLabelProviderFactoryExtensions
    {
        /// <summary>
        /// <see cref="IEnumLabelProvider{TEnum}"/> オブジェクトの新しいインスタンスを作成します。
        /// </summary>
        /// <typeparam name="TEnum">列挙体の型。</typeparam>
        /// <param name="factory"><see cref="IEnumLabelProviderFactory"/> オブジェクト。</param>
        /// <returns><see cref="IEnumLabelProvider{TEnum}"/> オブジェクト。</returns>
        public static IEnumLabelProvider<TEnum> CreateProvider<TEnum>(this IEnumLabelProviderFactory factory)
            where TEnum : Enum
            => (IEnumLabelProvider<TEnum>)factory.CreateProvider(typeof(TEnum));
    }
}
