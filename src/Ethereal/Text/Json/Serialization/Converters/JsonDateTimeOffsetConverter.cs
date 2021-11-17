// Copyright (c) Ethereal. All rights reserved.

namespace System.Text.Json.Serialization
{
    /// <summary>
    /// JsonDateTimeOffsetConverter
    /// </summary>
    public class JsonDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        private readonly string? _dateFormatString = "yyyy-MM-dd HH:mm:ss";
        private readonly DateTimeOffsetConverterOptions? _dateTimeOffsetConverterOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDateTimeOffsetConverter"/> class.
        /// </summary>
        public JsonDateTimeOffsetConverter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDateTimeOffsetConverter"/> class.
        /// </summary>
        public JsonDateTimeOffsetConverter(string? dateFormatString) :
            this(DateTimeOffsetConverterOptions.AllowString, dateFormatString)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDateTimeOffsetConverter"/> class.
        /// </summary>
        public JsonDateTimeOffsetConverter(DateTimeOffsetConverterOptions? dateTimeOffsetConverterOptions) :
            this(dateTimeOffsetConverterOptions, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDateTimeOffsetConverter"/> class.
        /// </summary>
        public JsonDateTimeOffsetConverter(
            DateTimeOffsetConverterOptions? dateTimeOffsetConverterOptions,
            string? dateFormatString)
        {
            _dateTimeOffsetConverterOptions = dateTimeOffsetConverterOptions;
            _dateFormatString = string.IsNullOrWhiteSpace(dateFormatString) ? _dateFormatString : dateFormatString;
        }

        /// <inheritdoc/>
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => _dateTimeOffsetConverterOptions switch
            {
                DateTimeOffsetConverterOptions.AllowString when reader.TokenType == JsonTokenType.String => reader.GetDateTimeOffset(),
                DateTimeOffsetConverterOptions.AllowSeconds when reader.TokenType == JsonTokenType.Number => DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64()),
                DateTimeOffsetConverterOptions.AllowMilliseconds when reader.TokenType == JsonTokenType.Number => DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64()),
                _ => reader.GetDateTimeOffset(),
            };

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            switch (_dateTimeOffsetConverterOptions)
            {
                case DateTimeOffsetConverterOptions.AllowString:
                    writer.WriteStringValue(value.ToString(_dateFormatString));
                    break;

                case DateTimeOffsetConverterOptions.AllowSeconds:
                    writer.WriteNumberValue(value.ToUnixTimeSeconds());
                    break;

                case DateTimeOffsetConverterOptions.AllowMilliseconds:
                    writer.WriteNumberValue(value.ToUnixTimeMilliseconds());
                    break;

                default:
                    writer.WriteStringValue(value);
                    break;
            }
        }
    }
}