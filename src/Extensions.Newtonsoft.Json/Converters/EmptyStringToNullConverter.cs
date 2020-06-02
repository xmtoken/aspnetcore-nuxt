using Newtonsoft.Json;
using System;

namespace AspNetCoreNuxt.Extensions.Newtonsoft.Json.Converters
{
    /// <summary>
    /// JSON から値を読み込むときに空文字を null に変換するコンバーターを表します。
    /// </summary>
    public class EmptyStringToNullConverter : JsonConverter<string>
    {
        /// <inheritdoc/>
        public override string ReadJson(JsonReader reader, Type objectType, string existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = reader.Value?.ToString();
            return string.IsNullOrEmpty(value) ? null : value;
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, string value, JsonSerializer serializer)
            => writer.WriteValue(value);
    }
}
