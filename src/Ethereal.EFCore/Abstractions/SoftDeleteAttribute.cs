// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// Marks a class use SoftDelete
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class SoftDeleteAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoftDeleteAttribute"/> class.
        /// </summary>
        public SoftDeleteAttribute(string columnName = "is_deleted", string comment = "soft deleted")
        {
            ColumnName = Check.NotEmpty(columnName, nameof(columnName));
            Comment = comment ?? string.Empty;
        }

        /// <summary>
        /// Comment for the column
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Used to identify whether the column is soft deleted
        /// </summary>
        public string ColumnName { get; set; }
    }
}