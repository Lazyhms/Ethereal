// Copyright (c) Ethereal. All rights reserved.

using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AspNetCore.Mvc.ModelBinding
{
    /// <summary>
    /// ModelBinderProvider extensions
    /// </summary>
    public static class ModelBinderProviderExtensions
    {
        /// <summary>
        /// UseDefaultModelBinderProviders
        /// </summary>
        public static IList<IModelBinderProvider> UseDefaultModelBinderProviders(
            this IList<IModelBinderProvider> modelBinderProviders)
        {
            Check.NotNull(modelBinderProviders, nameof(modelBinderProviders));

            modelBinderProviders.WithModelBinderProvider(new GuidModelBinderProvider());
            modelBinderProviders.WithModelBinderProvider(new DateTimeOffsetModelBinderProvider());

            return modelBinderProviders;
        }

        /// <summary>
        /// WithModelBinderProvider
        /// </summary>
        public static IList<IModelBinderProvider> WithModelBinderProvider<T>(
            this IList<IModelBinderProvider> modelBinderProviders,
            T modelBinderProvider) where T : IModelBinderProvider
        {
            Check.NotNull(modelBinderProviders, nameof(modelBinderProviders));
            Check.NotNull(modelBinderProvider, nameof(modelBinderProvider));

            if (modelBinderProviders.SingleOrDefault(cv => cv.GetType() == typeof(T)) is IModelBinderProvider provider)
            {
                modelBinderProviders.Remove(provider);
            }
            modelBinderProviders.Insert(0, modelBinderProvider);

            return modelBinderProviders;
        }
    }
}