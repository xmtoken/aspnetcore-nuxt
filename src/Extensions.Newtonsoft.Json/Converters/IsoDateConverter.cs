using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

namespace AspNetCoreNuxt.Extensions.Newtonsoft.Json.Converters
{
    /// <summary>
    /// 日時の日付のみを扱うコンバーターを表します。
    /// </summary>
    public class IsoDateConverter : IsoDateTimeConverter
    {
        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = base.ReadJson(reader, objectType, existingValue, serializer);
            switch (value)
            {
                case DateTime dateTime:
                    if (DateTimeStyles.HasFlag(DateTimeStyles.AdjustToUniversal) ||
                        DateTimeStyles.HasFlag(DateTimeStyles.AssumeUniversal))
                    {
                        return dateTime.ToUniversalTime().Date;
                    }
                    else
                    {
                        return dateTime.Date;
                    }
                case DateTimeOffset dateTimeOffset:
                    if (DateTimeStyles.HasFlag(DateTimeStyles.AdjustToUniversal) ||
                        DateTimeStyles.HasFlag(DateTimeStyles.AssumeUniversal))
                    {
                        return dateTimeOffset.ToUniversalTime().Date;
                    }
                    else
                    {
                        return dateTimeOffset.Date;
                    }
                default:
                    return value;
            }
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            switch (value)
            {
                case DateTime dateTime:
                    if (DateTimeStyles.HasFlag(DateTimeStyles.AdjustToUniversal) ||
                        DateTimeStyles.HasFlag(DateTimeStyles.AssumeUniversal))
                    {
                        base.WriteJson(writer, dateTime.ToUniversalTime().Date, serializer);
                    }
                    else
                    {
                        base.WriteJson(writer, dateTime.Date, serializer);
                    }
                    break;
                case DateTimeOffset dateTimeOffset:
                    if (DateTimeStyles.HasFlag(DateTimeStyles.AdjustToUniversal) ||
                        DateTimeStyles.HasFlag(DateTimeStyles.AssumeUniversal))
                    {
                        base.WriteJson(writer, dateTimeOffset.ToUniversalTime().Date, serializer);
                    }
                    else
                    {
                        base.WriteJson(writer, dateTimeOffset.Date, serializer);
                    }
                    break;
                default:
                    base.WriteJson(writer, value, serializer);
                    break;
            }
        }
    }
}
