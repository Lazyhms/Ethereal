// Copyright (c) Ethereal. All rights reserved.
//

namespace System.Text.Json.Serialization
{
    /// <summary>
    /// JsonConverterDateTimeAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class JsonDateTimeConverterAttribute : JsonConverterAttribute
    {
        /// <summary>
        /// DateFormatString
        /// </summary>
        public string? DateFormatString { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDateTimeConverterAttribute"/> class.
        /// </summary>
        public JsonDateTimeConverterAttribute(string? dateFormatString) 
            => DateFormatString = dateFormatString;

        /// <inheritdoc/>
        public override JsonConverter? CreateConverter(Type typeToConvert) 
            => new JsonDateTimeConverter(DateFormatString);
    }
}
