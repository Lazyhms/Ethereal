// Copyright (c) Ethereal. All rights reserved.

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
        public SoftDeleteAttribute(string columnName = "IsDeleted", string comment = "soft deleted")
        {
            Check.NotEmpty(columnName, nameof(columnName));

            ColumnName = columnName;
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