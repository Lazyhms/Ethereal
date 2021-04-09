﻿// Copyright (c) Ethereal. All rights reserved.

using Ethereal.Utilities;
using System;

namespace Ethereal.EntityFrameworkCore
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
        public SoftDeleteAttribute(string isDeleted = "IsDeleted", string comment = "soft deleted")
        {
            Check.NotEmpty(isDeleted, nameof(isDeleted));

            IsDeleted = isDeleted;
            Comment = comment ?? string.Empty;
        }

        /// <summary>
        /// Comment for the column
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Used to identify whether the column is soft deleted
        /// </summary>
        public string IsDeleted { get; set; }
    }
}