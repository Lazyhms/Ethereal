// Copyright (c) Ethereal. All rights reserved.
//

using System;

namespace Microsoft.AspNetCore.Mvc.ModelBinding
{
    /// <summary>
    /// DateTimeOffsetBinderOptions
    /// </summary>
    [Flags]
    public enum DateTimeOffsetBinderOptions
    {
        /// <summary>
        /// SupportSeconds
        /// </summary>
        SupportSeconds = 0x1,
        /// <summary>
        /// SupportMilliseconds
        /// </summary>
        SupportMilliseconds = 0x2
    }
}
