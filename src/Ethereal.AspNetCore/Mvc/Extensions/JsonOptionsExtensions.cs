// Copyright (c) Ethereal. All rights reserved.

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// JsonOptions extensions
    /// </summary>
    public static class JsonOptionsExtensions
    {
        /// <summary>
        /// UseDefaultJsonOptions
        /// </summary>
        public static JsonOptions UseDefaultJsonOptions(this JsonOptions jsonOptions)
        {
            jsonOptions.JsonSerializerOptions.AllowTrailingCommas = true;
            jsonOptions.JsonSerializerOptions.WriteIndented = true;
            jsonOptions.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            jsonOptions.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
            jsonOptions.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
            jsonOptions.JsonSerializerOptions.Converters.UseDefaultConverter();

            return jsonOptions;
        }
    }
}