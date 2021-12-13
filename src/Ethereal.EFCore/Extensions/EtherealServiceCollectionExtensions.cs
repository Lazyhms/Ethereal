// Copyright (c) Ethereal. All rights reserved.

using Ethereal.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
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
                .TryAdd<ISingletonOptions, IEtherealOptions>(p => p.GetRequiredService<IEtherealOptions>())
                .TryAdd<IConventionSetPlugin, EtherealConventionSetPlugin>()
                .TryAddProviderSpecificServices(p => p
                    .TryAddSingleton<IEtherealOptions, EtherealOptions>());

            return serviceCollection;
        }
    }
}