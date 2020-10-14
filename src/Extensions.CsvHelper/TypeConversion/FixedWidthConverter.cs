using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Reflection;
using System.Text;

namespace AspNetCoreNuxt.Extensions.CsvHelper.TypeConversion
{
    /// <summary>
    /// 値を固定長の文字列へ変換するコンバーターを表します。
    /// </summary>
    public class FixedWidthConverter : DefaultTypeConverter
    {
        /// <summary>
        /// <see cref="System.Text.Encoding"/> オブジェクトを表します。。
        /// </summary>
        private readonly Encoding Encoding;

        /// <summary>
        /// 固定長のバイト数を表します。
        /// </summary>
        private readonly int Length;

        /// <summary>
        /// <see cref="FixedWidthConverter"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="encoding"><see cref="System.Text.Encoding"/> オブジェクト。</param>
        /// <param name="length">固定長のバイト数。</param>
        public FixedWidthConverter(Encoding encoding, int length)
        {
            Encoding = encoding;
            Length = length;
        }

        /// <inheritdoc/>
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            var characters = base.ConvertToString(value, row, memberMapData);

            var builder = new StringBuilder(Length);
            var spaceByteCount = Length;

            foreach (var character in characters)
            {
                var characterByteCount = Encoding.GetByteCount(new[] { character });
                if (characterByteCount > spaceByteCount)
                {
                    break;
                }
                builder.Append(character);
                spaceByteCount -= characterByteCount;
            }

            if (spaceByteCount > 0)
            {
                var property = (PropertyInfo)memberMapData.Member;
                var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                if (propertyType == typeof(decimal) ||
                    propertyType == typeof(double) ||
                    propertyType == typeof(float) ||
                    propertyType == typeof(int) ||
                    propertyType == typeof(uint) ||
                    propertyType == typeof(long) ||
                    propertyType == typeof(ulong) ||
                    propertyType == typeof(short) ||
                    propertyType == typeof(ushort))
                {
                    builder.Insert(0, new string('0', spaceByteCount));
                }
                else
                {
                    builder.Append(new string(' ', spaceByteCount));
                }
            }

            return builder.ToString();
        }
    }
}
