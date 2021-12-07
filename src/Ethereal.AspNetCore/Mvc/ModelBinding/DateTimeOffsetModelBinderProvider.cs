// Copyright (c) Ethereal. All rights reserved.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.AspNetCore.Mvc.ModelBinding
{
    /// <summary>
    /// DateTimeOffsetModelBinderProvider
    /// </summary>
    public class DateTimeOffsetModelBinderProvider : IModelBinderProvider
    {
        private readonly DateTimeOffsetBinderOptions? _dateTimeOffsetBinderOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeOffsetModelBinderProvider"/> class.
        /// </summary>
        public DateTimeOffsetModelBinderProvider(DateTimeOffsetBinderOptions? dateTimeOffsetBinderOptions = default)
            => _dateTimeOffsetBinderOptions = dateTimeOffsetBinderOptions;

        /// <inheritdoc/>
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            Check.NotNull(context, nameof(context));

            var modelType = context.Metadata.UnderlyingOrModelType;
            if (typeof(DateTimeOffset) == modelType)
            {
                var loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
                return new DateTimeOffsetModelBinder(_dateTimeOffsetBinderOptions, loggerFactory);
            }
            return default;
        }
    }
}