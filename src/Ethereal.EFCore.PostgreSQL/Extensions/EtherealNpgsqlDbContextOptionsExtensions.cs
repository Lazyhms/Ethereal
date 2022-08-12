// Copyright (c) Ethereal. All rights reserved.

using Ethereal.EntityFrameworkCore.Infrastructure;
using Ethereal.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore;

/// <summary>
/// DbContextOptions extensions
/// </summary>
public static class EtherealNpgsqlDbContextOptionsExtensions
{
    /// <summary>
    /// Extension use Ethereal
    /// </summary>
    public static DbContextOptionsBuilder UseEtherealNpgsql(
        this DbContextOptionsBuilder optionsBuilder,
        Action<EtherealNpgsqlDbContextOptionsBuilder>? etherealOptionsAction = default)
    {
        Check.NotNull(optionsBuilder, nameof(optionsBuilder));

        ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(GetOrCreateExtension(optionsBuilder));

        ConfigureWarnings(optionsBuilder);

        etherealOptionsAction?.Invoke(new EtherealNpgsqlDbContextOptionsBuilder(optionsBuilder));

        return optionsBuilder;
    }

    /// <summary>
    /// ConfigureWarnings
    /// </summary>
    private static void ConfigureWarnings(DbContextOptionsBuilder optionsBuilder)
    {
        var coreOptionsExtension = optionsBuilder.Options.FindExtension<CoreOptionsExtension>() ?? new CoreOptionsExtension();
        ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(coreOptionsExtension);
    }

    /// <summary>
    /// GetOrCreateExtension
    /// </summary>
    private static EtherealNpgsqlOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder optionsBuilder)
     => optionsBuilder.Options.FindExtension<EtherealNpgsqlOptionsExtension>() ?? new EtherealNpgsqlOptionsExtension();
}