// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Ethereal.EntityFrameworkCore.Infrastructure.Internal
{
    /// <summary>
    /// EtherealOptions Extension
    /// </summary>
    public class EtherealOptionsExtension : IDbContextOptionsExtension
    {
        private DbContextOptionsExtensionInfo? _info;

        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealOptionsExtension"/> class.
        /// </summary>
        public EtherealOptionsExtension()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealOptionsExtension"/> class.
        /// </summary>
        public EtherealOptionsExtension(EtherealOptionsExtension copyFrom)
        {
        }

        /// <inheritdoc/>
        public DbContextOptionsExtensionInfo Info => _info ??= new ExtensionInfo(this);

        /// <inheritdoc/>
        public void ApplyServices(IServiceCollection services) => services.AddEntityFrameworkCoreRelationalServices();

        /// <inheritdoc/>
        public void Validate(IDbContextOptions options)
        {
        }

        /// <summary>
        /// WithServerVersion
        /// </summary>
        public virtual EtherealOptionsExtension UseSequence()
        {
            var clone = Clone();
            return clone;
        }

        /// <summary>
        /// clone
        /// </summary>
        protected EtherealOptionsExtension Clone() => new(this);

        private sealed class ExtensionInfo : DbContextOptionsExtensionInfo
        {
            public ExtensionInfo(IDbContextOptionsExtension extension) : base(extension)
            {
            }

            public new EtherealOptionsExtension Extension => (EtherealOptionsExtension)base.Extension;

            public override bool IsDatabaseProvider => false;

            public override string LogFragment => string.Empty;

            public override long GetServiceProviderHashCode() => 0;

            public override void PopulateDebugInfo(IDictionary<string, string> debugInfo) => debugInfo["Ethereal"] = "0";
        }
    }
}