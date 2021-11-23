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
        NONE = 0,
        /// <summary>
        /// Lower
        /// </summary>
        LOWERCASE = 0x1,
        /// <summary>
        /// Upper
        /// </summary>
        UPPERCASE = 0x2
    }
}
