// Copyright (c) Ethereal. All rights reserved.

using System.Reflection;

namespace System.Text.Json.Serialization
{
    /// <summary>
    /// JsonConverterMethodBase
    /// </summary>
    public class JsonMethodBaseConverter : JsonConverter<MethodBase?>
    {
        /// <inheritdoc/>
        public override MethodBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, MethodBase? value, JsonSerializerOptions options) => writer.WriteNullValue();
    }
}