// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore;
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
        /// NamingPolicy
        /// </summary>
        internal NamingPolicy NamingPolicy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealOptionsExtension"/> class.
        /// </summary>
        public EtherealOptionsExtension()
            => NamingPolicy = NamingPolicy.None;

        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealOptionsExtension"/> class.
        /// </summary>
        public EtherealOptionsExtension(EtherealOptionsExtension copyFrom)
            => NamingPolicy = copyFrom.NamingPolicy;

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
        protected EtherealOptionsExtension Clone()
            => new(this);

        /// <summary>
        /// WithNamingPolicy
        /// </summary>
        public virtual EtherealOptionsExtension WithNamingPolicy(NamingPolicy namingPolicy)
        {
            var clone = Clone();
            clone.NamingPolicy = namingPolicy;
            return clone;
        }

        private sealed class ExtensionInfo : DbContextOptionsExtensionInfo
        {
            public ExtensionInfo(IDbContextOptionsExtension extension)
                : base(extension)
            {
            }

            private new EtherealOptionsExtension Extension
                => (EtherealOptionsExtension)base.Extension;

            public override bool IsDatabaseProvider
                => false;

            public override string LogFragment
                => "using Ethereal";

            public override int GetServiceProviderHashCode()
                => 0;

            public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
                => debugInfo["Ethereal:" + nameof(EtherealDbContextOptionsExtensions.UseEthereal)] = "1";

            public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
                => other is ExtensionInfo;
        }
    }
}