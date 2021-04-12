// Copyright (c) Ethereal. All rights reserved.

using System.Collections.Generic;
using System.Linq;

namespace System.Text.Json.Serialization
{
    /// <summary>
    /// JsonConverter extensions
    /// </summary>
    public static class JsonConverterExtensions
    {
        /// <summary>
        /// Default Conveters
        /// </summary>
        public static IList<JsonConverter> UseDefaultConverter(
            this IList<JsonConverter> converters)
        {
            Check.NotNull(converters, nameof(converters));

            converters.Add(new JsonGuidConverter());

            converters.Add(new JsonStringConverter());

            converters.Add(new JsonDateTimeConverter());
            converters.Add(new JsonDateTimeOffsetConverter(DateTimeOffsetConverterOptions.AllowString));

            converters.Add(new JsonMethodBaseConverter());

            return converters;
        }

        /// <summary>
        /// JsonConverter
        /// </summary>
        public static IList<JsonConverter> WithConverter<T>(
            this IList<JsonConverter> converters,
            T jsonConverter) where T : JsonConverter
        {
            Check.NotNull(converters, nameof(converters));
            Check.NotNull(jsonConverter, nameof(jsonConverter));

            if (converters.SingleOrDefault(cv => cv.GetType() == typeof(T)) is JsonConverter converter)
            {
                converters.Remove(converter);
            }
            converters.Add(jsonConverter);

            return converters;
        }
    }
}