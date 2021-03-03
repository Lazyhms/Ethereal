// Copyright (c) Ethereal. All rights reserved.
//

namespace System.Text.Json.Serialization
{
    /// <summary>
    /// JsonConverterDateTime
    /// </summary>
    public class JsonDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string? _dateFormatString = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDateTimeConverter"/> class.
        /// </summary>
        public JsonDateTimeConverter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDateTimeConverter"/> class.
        /// </summary>
        public JsonDateTimeConverter(string? dateFormatString)
            => _dateFormatString = string.IsNullOrWhiteSpace(dateFormatString) ? _dateFormatString : dateFormatString;

        /// <inheritdoc/>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            reader.GetDateTime();


        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(_dateFormatString));
    }
}
