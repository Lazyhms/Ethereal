﻿// Copyright (c) Ethereal. All rights reserved.

using System;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// ConfigurationBinder
    /// </summary>
    public static class ConfigurationBinderExtensions
    {
        /// <summary>
        /// Bind
        /// </summary>
        public static T Bind<T>(
            this IConfiguration configuration) where T : class, new()
        {
            Check.NotNull(configuration, nameof(configuration));

            var objectinstac = Activator.CreateInstance<T>();
            configuration.Bind(objectinstac);
            return objectinstac;
        }

        /// <summary>
        /// Bind
        /// </summary>
        public static T Bind<T>(
            this IConfiguration configuration,
            string key) where T : class, new()
        {
            Check.NotNull(configuration, nameof(configuration));

            var objectinstac = Activator.CreateInstance<T>();
            configuration.Bind(key, objectinstac);
            return objectinstac;
        }
    }
}