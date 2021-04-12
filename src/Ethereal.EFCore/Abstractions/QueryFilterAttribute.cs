// Copyright (c) Ethereal. All rights reserved.

using System;

namespace Ethereal.EntityFrameworkCore
{
    /// <summary>
    /// Marks a property or field with a query filter which will be use in query .
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class QueryFilterAttribute : Attribute
    {
        /// <summary>
        /// QueryFilter method
        /// </summary>
        public QueryFilterAttribute(bool value) => Value = value;

        /// <summary>
        /// The bool value
        /// </summary>
        public bool Value { get; set; }
    }
}