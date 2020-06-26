using System;

namespace AspNetCoreNuxt.Extensions
{
    /// <inheritdoc/>
    /// <typeparam name="TEnum">列挙体の型。</typeparam>
    public interface IEnumLabelProvider<TEnum> : IEnumLabelProvider
        where TEnum : Enum
    {
        /// <inheritdoc cref="IEnumLabelProvider.CreateLabel(object)"/>
        string CreateLabel(TEnum value);
    }
}
