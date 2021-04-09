﻿// Copyright (c) Ethereal. All rights reserved.

using System;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc.Formatters
{
    /// <summary>
    /// Reads an object from a request body with a text plain format.
    /// </summary>
    public class TextPlainInputFormatter : TextInputFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextPlainInputFormatter"/> class.
        /// </summary>
        public TextPlainInputFormatter()
        {
            SupportedMediaTypes.Add("text/plain");
            SupportedEncodings.Add(UTF8EncodingWithoutBOM);
            SupportedEncodings.Add(UTF16EncodingLittleEndian);
        }

        /// <inheritdoc/>
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            string? data = null;
            using (var streamReader = context.ReaderFactory(context.HttpContext.Request.Body, encoding))
            {
                data = await streamReader.ReadToEndAsync();
            }
            return InputFormatterResult.Success(data);
        }

        /// <inheritdoc/>
        protected override bool CanReadType(Type type) => type == typeof(string);
    }
}