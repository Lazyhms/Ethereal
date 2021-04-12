// Copyright (c) Ethereal. All rights reserved.

using System;

namespace Ethereal.EntityFrameworkCore
{
    /// <summary>
    /// Marks a property or field with a sequence value which will be included in the SQL sent to
    /// the database .
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class SequenceValueAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceValueAttribute"/> class.
        /// </summary>
        public SequenceValueAttribute(string name)
        {
            Check.NotEmpty(name, nameof(name));

            Name = name;
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Schema
        /// </summary>
        public string? Schema { get; set; }
    }
}