// Copyright (c) Ethereal. All rights reserved.

using System;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// Naming policy
    /// </summary>
    [Flags]
    public enum NamingPolicy
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// Lower
        /// </summary>
        LowerCase = 0x1,
        /// <summary>
        /// Upper
        /// </summary>
        UpperCase = 0x2
    }
}
