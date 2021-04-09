// Copyright (c) Ethereal. All rights reserved.

namespace System.Text.Json.Serialization
{
    /// <summary>
    /// JsonConverterDateTimeAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class JsonDateTimeOffsetConverterAttribute : JsonConverterAttribute
    {
        /// <summary>
        /// DateTimeOffsetConverterOptions
        /// </summary>
        public DateTimeOffsetConverterOptions? DateTimeOffsetConverterOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDateTimeOffsetConverterAttribute"/> class.
        /// </summary>
        public JsonDateTimeOffsetConverterAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDateTimeConverterAttribute"/> class.
        /// </summary>
        public JsonDateTimeOffsetConverterAttribute(string dateFormatString)
            => DateFormatString = dateFormatString;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDateTimeOffsetConverterAttribute"/> class.
        /// </summary>
        public JsonDateTimeOffsetConverterAttribute(DateTimeOffsetConverterOptions dateTimeOffsetConverterOptions)
            => DateTimeOffsetConverterOptions = dateTimeOffsetConverterOptions;

        /// <summary>
        /// DateFormatString
        /// </summary>
        public string? DateFormatString { get; set; }

        /// <inheritdoc/>
        public override JsonConverter? CreateConverter(Type typeToConvert)
            => new JsonDateTimeOffsetConverter(DateTimeOffsetConverterOptions, DateFormatString);
    }
}