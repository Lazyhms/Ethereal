// Copyright (c) Ethereal. All rights reserved.

namespace System.Text.Json.Serialization
{
    /// <summary>
    /// JsonConverterString
    /// </summary>
    public sealed class JsonStringConverter : JsonConverter<string?>
    {
        /// <inheritdoc/>
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.TokenType == JsonTokenType.Null ? string.Empty : reader.GetString();

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
            => writer.WriteStringValue(value ?? string.Empty);
    }
}