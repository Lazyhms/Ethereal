// Copyright (c) Ethereal. All rights reserved.

using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

namespace System.Text.Json
{
    /// <summary>
    /// Json Options
    /// </summary>
    public static class JsonOptions
    {
        static JsonOptions()
        {
            DefaultSerializerOptions.Converters.Add(new JsonGuidConverter());
            DefaultSerializerOptions.Converters.Add(new JsonStringConverter());
            DefaultSerializerOptions.Converters.Add(new JsonDateTimeConverter());
            DefaultSerializerOptions.Converters.Add(new JsonDateTimeOffsetConverter());
        }


        /// <summary>
        /// Default serializer options
        /// </summary>

        public static JsonSerializerOptions DefaultSerializerOptions = new()
        {
            WriteIndented = true,
            AllowTrailingCommas = true,
            PropertyNamingPolicy = null,
            PropertyNameCaseInsensitive = false,
            ReadCommentHandling = JsonCommentHandling.Skip,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        };

    }
}
