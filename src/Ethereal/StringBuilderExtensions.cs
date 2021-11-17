// Copyright (c) Ethereal. All rights reserved.

using System.Collections.Generic;

namespace System.Text
{
    /// <summary>
    /// StringBuilder extensions
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Appends a copy of the specified string to this instance when condition is true.
        /// </summary>
        public static StringBuilder Append<T>(this StringBuilder builder, bool condition, T value)
            => condition ? builder.Append(value) : builder;

        /// <summary>
        /// Appends a copy of the specified string to this instance when condition is true.
        /// </summary>
        public static StringBuilder AppendLine(this StringBuilder builder, bool condition, string value)
            => condition ? builder.AppendLine(value) : builder;

        /// <summary>
        /// Appends a copy of the specified string to this instance.
        /// </summary>
        public static StringBuilder AppendJoin<T>(
                    this StringBuilder stringBuilder,
                    IEnumerable<T?> values,
                    Action<StringBuilder, T?> joinAction,
                    string separator = ", ")
        {
            var appended = false;

            foreach (var value in values)
            {
                joinAction(stringBuilder, value);
                stringBuilder.Append(separator);
                appended = true;
            }

            if (appended)
            {
                stringBuilder.Length -= separator.Length;
            }

            return stringBuilder;
        }
    }
}
