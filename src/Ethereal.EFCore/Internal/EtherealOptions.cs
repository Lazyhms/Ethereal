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
        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealOptions"/> class.
        /// </summary>
        public EtherealOptions()
        {
        }

        /// <inheritdoc/>
        public void Initialize(IDbContextOptions options)
        {
            var etherealOptionsExtension = options.FindExtension<EtherealOptionsExtension>() ?? new EtherealOptionsExtension();
        }

        /// <inheritdoc/>
        public void Validate(IDbContextOptions options)
        {
            var etherealOptionsExtension = options.FindExtension<EtherealOptionsExtension>() ?? new EtherealOptionsExtension();
        }
    }
}
