// Copyright (c) Ethereal. All rights reserved.

using Ethereal.EntityFrameworkCore;
using System;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// DateTimeExtensions
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Between
        /// </summary>
        public static bool Between(this DateTime _, DateTime start, DateTime end) =>
            throw new InvalidOperationException(string.Format(CoreStrings.FunctionOnClient, nameof(Between)));

        /// <summary>
        /// Between
        /// </summary>
        public static bool Between(this DateTime? _, DateTime start, DateTime end) =>
            throw new InvalidOperationException(string.Format(CoreStrings.FunctionOnClient, nameof(Between)));
    }
}