// Copyright (c) Ethereal. All rights reserved.

using Ethereal.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace Microsoft.EntityFrameworkCore.Internal
{
    /// <summary>
    /// EtherealOptions
    /// </summary>
    public class EtherealOptions : IEtherealOptions
    {
        /// <inheritdoc/>
        public virtual NamingPolicy NamingPolicy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealOptions"/> class.
        /// </summary>
        public EtherealOptions()
            => NamingPolicy = NamingPolicy.None;

        /// <inheritdoc/>
        public void Initialize(IDbContextOptions options)
        {
            var etherealOptionsExtension = options.FindExtension<EtherealOptionsExtension>() ?? new EtherealOptionsExtension();
            NamingPolicy = etherealOptionsExtension.NamingPolicy;
        }

        /// <inheritdoc/>
        public void Validate(IDbContextOptions options)
        {
            var etherealOptionsExtension = options.FindExtension<EtherealOptionsExtension>() ?? new EtherealOptionsExtension();
        }
    }
}
