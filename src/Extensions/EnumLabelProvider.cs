using System;

namespace AspNetCoreNuxt.Extensions
{
    /// <inheritdoc cref="IEnumLabelProvider{TEnum}"/>
    public abstract class EnumLabelProvider<TEnum> : IEnumLabelProvider<TEnum>
        where TEnum : Enum
    {
        /// <inheritdoc/>
        public virtual string CreateLabel(object value)
            => CreateLabel((TEnum)value);

        /// <inheritdoc/>
        public abstract string CreateLabel(TEnum value);
    }
}
