using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Reflection;

namespace AspNetCoreNuxt.Extensions.CsvHelper.TypeConversion
{
    ///// <summary>
    ///// <see cref="Enum"/> 型の値とラベルテキストを変換するコンバーターを表します。
    ///// </summary>
    ///// <typeparam name="TEnum">列挙体の型。</typeparam>
    //public class EnumLabelConverter<TEnum> : DefaultTypeConverter
    //    where TEnum : struct, Enum
    //{
    //    /// <summary>
    //    /// <see cref="IEnumLabelProvider{TEnum}"/> オブジェクトを表します。
    //    /// </summary>
    //    private readonly IEnumLabelProvider<TEnum> EnumLabelProvider;

    //    /// <summary>
    //    /// <see cref="EnumLabelConverter{T}"/> クラスの新しいインスタンスを作成します。
    //    /// </summary>
    //    /// <param name="enumLabelProviderFactory"><see cref="IEnumLabelProviderFactory"/> オブジェクト。</param>
    //    public EnumLabelConverter(IEnumLabelProviderFactory enumLabelProviderFactory)
    //    {
    //        EnumLabelProvider = enumLabelProviderFactory.CreateProvider<TEnum>();
    //    }

    //    /// <summary>
    //    /// <see cref="EnumLabelConverter{T}"/> クラスの新しいインスタンスを作成します。
    //    /// </summary>
    //    /// <param name="enumLabelProvider"><see cref="IEnumLabelProvider{TEnum}"/> オブジェクト。</param>
    //    public EnumLabelConverter(IEnumLabelProvider<TEnum> enumLabelProvider)
    //    {
    //        EnumLabelProvider = enumLabelProvider;
    //    }

    //    /// <inheritdoc/>
    //    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    //    {
    //        switch (memberMapData.Member)
    //        {
    //            case FieldInfo field:
    //                if (field.FieldType.IsNullable() && string.IsNullOrEmpty(text))
    //                {
    //                    return null;
    //                }
    //                break;

    //            case PropertyInfo property:
    //                if (property.PropertyType.IsNullable() && string.IsNullOrEmpty(text))
    //                {
    //                    return null;
    //                }
    //                break;
    //        }

    //        foreach (var value in Enum.GetValues(typeof(TEnum)))
    //        {
    //            if (EnumLabelProvider.CreateLabel(value) == text)
    //            {
    //                return value;
    //            }
    //        }

    //        return Enum.Parse<TEnum>(text, ignoreCase: true);
    //    }

    //    /// <inheritdoc/>
    //    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    //    {
    //        if (value == null)
    //        {
    //            return null;
    //        }
    //        return EnumLabelProvider.CreateLabel(value);
    //    }
    //}
}
