// Copyright (c) Ethereal. All rights reserved.
//

namespace System.Text.Json.Serialization
{
    /// <summary>
    /// JsonConverterGuidAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class JsonGuidConverterAttribute : JsonConverterAttribute
    {
        /// <summary>
        /// GuidConverterOptions
        /// </summary>
        public GuidConverterOptions? GuidConverterOptions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonGuidConverterAttribute"/> class.
        /// </summary>
        public JsonGuidConverterAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonGuidConverterAttribute"/> class.
        /// </summary>
        public JsonGuidConverterAttribute(GuidConverterOptions? guidConverterOptions)
            => GuidConverterOptions = guidConverterOptions;

        /// <inheritdoc/>
        public override JsonConverter? CreateConverter(Type typeToConvert)
            => new JsonGuidConverter(GuidConverterOptions);
    }
}
