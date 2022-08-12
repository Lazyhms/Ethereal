// Copyright (c) Ethereal. All rights reserved.

using Ethereal.EFCore.PostgreSQL.Query.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;

namespace Ethereal.EntityFrameworkCore;

/// <summary>
/// Ethereal ServiceCollection extensions
/// </summary>
public static class EtherealNpgsqlServiceCollectionExtensions
{
    /// <summary>
    /// Add EntityFrameworkCoreRelationalServices
    /// </summary>
    public static IServiceCollection AddEntityFrameworkCoreRelationalServices(this IServiceCollection serviceCollection)
    {
        Check.NotNull(serviceCollection, nameof(serviceCollection));

        new EntityFrameworkRelationalServicesBuilder(serviceCollection)
            .TryAdd<IMethodCallTranslatorPlugin, EtherealNpgsqlMethodCallTranslatorPlugin>();

        return serviceCollection;
    }
}