// Copyright (c) Ethereal. All rights reserved.

using Ethereal.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ethereal.EntityFrameworkCore
{
    /// <summary>
    /// Ethereal ServiceCollection extensions
    /// </summary>
    public static class EtherealServiceCollectionExtensions
    {
        /// <summary>
        /// Add EntityFrameworkCoreRelationalServices
        /// </summary>
        public static IServiceCollection AddEntityFrameworkCoreRelationalServices(this IServiceCollection serviceCollection)
        {
            Check.NotNull(serviceCollection, nameof(serviceCollection));

            new EntityFrameworkRelationalServicesBuilder(serviceCollection)
                .TryAdd<IConventionSetPlugin, EtherealConventionSetPlugin>()
                .TryAddProviderSpecificServices(
                    x =>
                        x.TryAddSingletonEnumerable<IMethodCallTranslatorPlugin, EtherealDateTimeMethodCallTranslatorPlugin>()
                );

            return serviceCollection;
        }
    }
}