// Copyright (c) Ethereal. All rights reserved.

using System;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// Marks a property or field which will be not update .
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class InsertIgnoreAttribute : Attribute
    {
    }
}