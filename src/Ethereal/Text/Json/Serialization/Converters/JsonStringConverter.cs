// Copyright (c) Ethereal. All rights reserved.

namespace System.Text.Json.Serialization
{
    /// <summary>
    /// JsonConverterString
    /// </summary>
    public sealed class JsonStringConverter : JsonConverter<string?>
    {
#if NET5_0

        /// <inheritdoc/>
        public override bool HandleNull => true;

#endif

        /// <inheritdoc/>
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return string.Empty;
            }
            return reader.GetString();
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
            => writer.WriteStringValue(value ?? string.Empty);
    }
}