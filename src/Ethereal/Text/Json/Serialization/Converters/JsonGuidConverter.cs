// Copyright (c) Ethereal. All rights reserved.

namespace System.Text.Json.Serialization
{
    /// <summary>
    /// JsonvConverterGuid
    /// </summary>
    public sealed class JsonGuidConverter : JsonConverter<Guid>
    {
        private readonly GuidConverterOptions? _converterOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonGuidConverter"/> class.
        /// </summary>
        public JsonGuidConverter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonGuidConverter"/> class.
        /// </summary>
        public JsonGuidConverter(GuidConverterOptions? converterOptions)
            => _converterOptions = converterOptions;

        /// <inheritdoc/>
        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TryGetGuid(out var value) || Guid.TryParse(reader.GetString(), out value))
            {
                return value;
            }
            return default;
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
        {
            switch (_converterOptions)
            {
                case GuidConverterOptions.N:
                    writer.WriteStringValue(value.ToString("N"));
                    break;

                case GuidConverterOptions.D:
                    writer.WriteStringValue(value.ToString("D"));
                    break;

                case GuidConverterOptions.B:
                    writer.WriteStringValue(value.ToString("B"));
                    break;

                case GuidConverterOptions.P:
                    writer.WriteStringValue(value.ToString("P"));
                    break;

                case GuidConverterOptions.X:
                    writer.WriteStringValue(value.ToString("X"));
                    break;

                default:
                    writer.WriteStringValue(value);
                    break;
            }
        }
    }
}