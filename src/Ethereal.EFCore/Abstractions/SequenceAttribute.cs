// Copyright (c) Ethereal. All rights reserved.

using Ethereal.Utilities;
using System;

namespace Ethereal.EntityFrameworkCore
{
    /// <summary>
    /// Create a sequence which will be included in the SQL sent to the database .
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class SequenceAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceAttribute"/> class.
        /// </summary>
        public SequenceAttribute(string name)
        {
            Check.NotEmpty(name, nameof(name));

            Name = name;
        }

        /// <summary>
        /// IncrementBy
        /// </summary>
        public int? IncrementBy { get; set; }

        /// <summary>
        /// Sets whether or not the sequence will start again from the beginning once the maximum
        /// value is reached.
        /// </summary>
        public bool? IsCyclic { get; set; }

        /// <summary>
        /// Maximum
        /// </summary>
        public long? Maximum { get; set; }

        /// <summary>
        /// Minimum
        /// </summary>
        public long? Minimum { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Schema
        /// </summary>
        public string? Schema { get; set; }

        /// <summary>
        /// StartValue
        /// </summary>
        public long? StartValue { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public Type? Type { get; set; }
    }
}