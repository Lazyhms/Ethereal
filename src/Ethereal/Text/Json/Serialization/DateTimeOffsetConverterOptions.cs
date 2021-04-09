// Copyright (c) Ethereal. All rights reserved.

namespace System.Text.Json.Serialization
{
    /// <summary>
    /// DateTimeOffsetConverterOptions
    /// </summary>
    [Flags]
    public enum DateTimeOffsetConverterOptions
    {
        /// <summary>
        /// None
        /// </summary>
        AllowString = 0x1,

        /// <summary>
        /// AllowSeconds
        /// </summary>
        AllowSeconds = 0x2,

        /// <summary>
        /// AllowMilliseconds
        /// </summary>
        AllowMilliseconds = 0x4
    }
}