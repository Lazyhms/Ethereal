// Copyright (c) Ethereal. All rights reserved.

namespace Microsoft.EntityFrameworkCore.Infrastructure.Internal
{
    /// <summary>
    /// Options
    /// </summary>
    public interface IEtherealOptions : ISingletonOptions
    {
        /// <summary>
        /// Naming policy
        /// </summary>
        NamingPolicy NamingPolicy { get; set; }
    }
}
