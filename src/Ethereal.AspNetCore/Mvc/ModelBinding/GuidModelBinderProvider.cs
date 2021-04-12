// Copyright (c) Ethereal. All rights reserved.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.AspNetCore.Mvc.ModelBinding.Binders
{
    /// <summary>
    /// Guid ModelBinder Provider
    /// </summary>
    public class GuidModelBinderProvider : IModelBinderProvider
    {
        /// <inheritdoc/>
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            Check.NotNull(context, nameof(context));

            var modelType = context.Metadata.UnderlyingOrModelType;
            if (modelType == typeof(Guid))
            {
                var loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
                return new GuidModelBinder(loggerFactory);
            }
            return default;
        }
    }
}