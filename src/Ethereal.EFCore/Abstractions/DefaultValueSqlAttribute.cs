// Copyright (c) Ethereal. All rights reserved.

using JetBrains.Annotations;
using System;

namespace Ethereal.EntityFrameworkCore
{
    /// <summary>
    /// Marks a property or field with a default value sql which will be included in the SQL sent to
    /// the database .
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class DefaultValueSqlAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultValueSqlAttribute"/> class.
        /// </summary>
        public DefaultValueSqlAttribute([CanBeNull] string? value) => Value = value;

        /// <summary>
        /// The value sql
        /// </summary>
        public string? Value { get; set; }
    }
}