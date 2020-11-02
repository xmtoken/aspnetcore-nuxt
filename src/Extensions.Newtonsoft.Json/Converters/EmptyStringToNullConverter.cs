using Newtonsoft.Json;
using System;

namespace AspNetCoreNuxt.Extensions.Newtonsoft.Json.Converters
{
    /// <summary>
    /// 空文字を null に変換するコンバーターを表します。
    /// </summary>
    public class EmptyStringToNullConverter : JsonConverter<string>
    {
        /// <inheritdoc/>
        public override string ReadJson(JsonReader reader, Type objectType, string existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = ReadJson(reader, objectType, existingValue, serializer) as string;
            return string.IsNullOrEmpty(value) ? null : value;
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, string value, JsonSerializer serializer)
            => WriteJson(writer, string.IsNullOrEmpty(value) ? (object)null : value, serializer);
    }
}
