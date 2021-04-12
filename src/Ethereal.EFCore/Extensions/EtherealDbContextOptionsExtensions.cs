// Copyright (c) Ethereal. All rights reserved.

using Ethereal.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// DbContextOptions extensions
    /// </summary>
    public static class EtherealDbContextOptionsExtensions
    {
        /// <summary>
        /// Extension use Ethereal
        /// </summary>
        public static DbContextOptionsBuilder UseEthereal(
            this DbContextOptionsBuilder optionsBuilder,
            Action<EtherealDbContextOptionsBuilder>? sqlServerOptionsAction = default)
        {
            Check.NotNull(optionsBuilder, nameof(optionsBuilder));

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(GetOrCreateExtension(optionsBuilder));

            ConfigureWarnings(optionsBuilder);

            sqlServerOptionsAction?.Invoke(new EtherealDbContextOptionsBuilder(optionsBuilder));

            return optionsBuilder;
        }

        /// <summary>
        /// ConfigureWarnings
        /// </summary>
        private static void ConfigureWarnings(DbContextOptionsBuilder optionsBuilder)
        {
            var coreOptionsExtension = optionsBuilder.Options.FindExtension<CoreOptionsExtension>() ?? new CoreOptionsExtension();
            //coreOptionsExtension = coreOptionsExtension.WithWarningsConfiguration(coreOptionsExtension.WarningsConfiguration.TryWithExplicit(RelationalEventId.AmbientTransactionWarning, WarningBehavior.Throw));
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(coreOptionsExtension);
        }

        /// <summary>
        /// GetOrCreateExtension
        /// </summary>
        private static EtherealOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder.Options.FindExtension<EtherealOptionsExtension>() ?? new EtherealOptionsExtension();
    }
}