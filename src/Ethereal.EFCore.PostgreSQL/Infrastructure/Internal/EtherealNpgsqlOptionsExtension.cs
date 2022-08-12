// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Ethereal.EntityFrameworkCore.Infrastructure.Internal;

/// <summary>
/// EtherealOptions Extension
/// </summary>
public class EtherealNpgsqlOptionsExtension : IDbContextOptionsExtension
{
    private DbContextOptionsExtensionInfo? _info;

    /// <summary>
    /// Initializes a new instance of the <see cref="EtherealNpgsqlOptionsExtension"/> class.
    /// </summary>
    public EtherealNpgsqlOptionsExtension() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="EtherealNpgsqlOptionsExtension"/> class.
    /// </summary>
    public EtherealNpgsqlOptionsExtension(EtherealNpgsqlOptionsExtension copyFrom) { }

    /// <inheritdoc/>
    public DbContextOptionsExtensionInfo Info
        => _info ??= new ExtensionInfo(this);

    /// <inheritdoc/>
    public void ApplyServices(IServiceCollection services)
        => services.AddEntityFrameworkCoreRelationalServices();

    /// <inheritdoc/>
    public void Validate(IDbContextOptions options)
    {
    }

    /// <summary>
    /// clone
    /// </summary>
    protected EtherealNpgsqlOptionsExtension Clone()
        => new(this);

    private sealed class ExtensionInfo : DbContextOptionsExtensionInfo
    {
        public ExtensionInfo(IDbContextOptionsExtension extension)
            : base(extension)
        {
        }

        private new EtherealNpgsqlOptionsExtension Extension
            => (EtherealNpgsqlOptionsExtension)base.Extension;

        public override bool IsDatabaseProvider
            => false;

        public override string LogFragment
            => "using Ethereal";

        public override int GetServiceProviderHashCode()
            => 0;

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            => debugInfo["Ethereal:" + nameof(EtherealNpgsqlDbContextOptionsExtensions.UseEtherealNpgsql)] = "1";

        public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
            => other is ExtensionInfo;
    }
}