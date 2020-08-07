using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Diagnostics.CodeAnalysis;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore.Storage.ValueConversion
{
    /// <summary>
    /// <see cref="Enum"/> 型の値と文字列を変換するコンバーターを表します。
    /// </summary>
    /// <typeparam name="TEnum">列挙体の型。</typeparam>
    /// <remarks>
    /// <see cref="EnumToStringConverter{TEnum}"/> クラスは <see cref="Enum.ToString()"/> メソッドを呼び出しますが、
    /// <see cref="EnumValueToStringConverter{TEnum}"/> クラスは <see cref="Enum.ToString(string)"/> メソッドを呼び出します。
    /// </remarks>
    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.")]
    public class EnumValueToStringConverter<TEnum> : StringEnumConverter<TEnum, string, TEnum>
        where TEnum : struct, Enum
    {
        /// <summary>
        /// <see cref="EnumValueToStringConverter{TEnum}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public EnumValueToStringConverter()
            : base(x => x.ToString("D"), ToEnum())
        {
        }
    }
}
