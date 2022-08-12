// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Microsoft.EntityFrameworkCore;

/// <summary>
/// EtherealNpgsqlIEnumerableDbFunctionsExtensions
/// </summary>
public static class EtherealNpgsqlIEnumerableDbFunctionsExtensions
{
    /// <summary>
    /// Unnset
    /// </summary>
    public static T Unnest<T>(this DbFunctions _, IEnumerable<T>? __)
    {
        throw new InvalidOperationException(CoreStrings.FunctionOnClient("Unnest"));
    }

    /// <summary>
    /// Any
    /// </summary>
    public static T Any<T>(this DbFunctions _, IEnumerable<T>? __) where T : struct
    {
        throw new InvalidOperationException(CoreStrings.FunctionOnClient("Any"));
    }
}
