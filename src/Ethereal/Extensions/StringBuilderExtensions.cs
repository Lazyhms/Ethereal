// Copyright (c) Ethereal. All rights reserved.

using System.Collections.Generic;
using System.Globalization;

namespace System.Text
{
    /// <summary>
    /// StringBuilder extensions
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Appends a string to this string builder when condition is true.
        /// </summary>
        public static StringBuilder Append(
            this StringBuilder builder,
            bool condition,
            string? value)
            => condition ? builder.Append(value) : builder;

        /// <summary>
        /// Appends a string to this string builder.
        /// </summary>
        public static StringBuilder Append(
            this StringBuilder builder,
            string? source,
            Action<StringBuilder, string?> appendAction)
        {
            appendAction(builder, source);
            return builder;
        }

        /// <summary>
        /// Appends a value to this string builder.
        /// </summary>
        public static StringBuilder Append<T>(
            this StringBuilder builder,
            T source,
            Action<StringBuilder, T> appendAction)
            where T : struct
        {
            appendAction(builder, source);
            return builder;
        }

        /// <summary>
        /// Appends a boolean to this string builder when condition is true.
        /// </summary>
        public static StringBuilder Append(
            this StringBuilder builder,
            bool condition,
            bool value)
            => condition ? builder.Append(value) : builder;

        /// <summary>
        /// Appends a sbyte to this string builder when condition is true.
        /// </summary>
        public static StringBuilder Append(
            this StringBuilder builder,
            bool condition,
            sbyte value)
            => condition ? builder.Append(value) : builder;

        /// <summary>
        /// Appends a byte to this string builder when condition is true.
        /// </summary>
        public static StringBuilder Append(
            this StringBuilder builder,
            bool condition,
            byte value)
            => condition ? builder.Append(value) : builder;

        /// <summary>
        /// Appends a char to this string builder when condition is true.
        /// </summary>
        public static StringBuilder Append(
            this StringBuilder builder,
            bool condition,
            char value)
            => condition ? builder.Append(value) : builder;

        /// <summary>
        /// Appends a short to this string builder when condition is true.
        /// </summary>
        public static StringBuilder Append(
            this StringBuilder builder,
            bool condition,
            short value)
            => condition ? builder.Append(value) : builder;

        /// <summary>
        /// Appends an int to this string builder when condition is true.
        /// </summary>
        public static StringBuilder Append(
            this StringBuilder builder,
            bool condition,
            int value)
            => condition ? builder.Append(value) : builder;

        /// <summary>
        /// Appends a long to this string builder when condition is true.
        /// </summary>
        public static StringBuilder Append(
            this StringBuilder builder,
            bool condition,
            long value)
            => condition ? builder.Append(value) : builder;

        /// <summary>
        /// Appends a float to this string builder when condition is true.
        /// </summary>
        public static StringBuilder Append(
            this StringBuilder builder,
            bool condition,
            float value)
            => condition ? builder.Append(value) : builder;

        /// <summary>
        /// Appends a double to this string builder when condition is true.
        /// </summary>
        public static StringBuilder Append(
            this StringBuilder builder,
            bool condition,
            double value)
            => condition ? builder.Append(value) : builder;

        /// <summary>
        /// Appends a decimal to this string builder when condition is true.
        /// </summary>
        public static StringBuilder Append(
            this StringBuilder builder,
            bool condition,
            decimal value)
            => condition ? builder.Append(value) : builder;

        /// <summary>
        /// Appends a datetime to this string builder when condition is true.
        /// </summary>
        public static StringBuilder Append(
            this StringBuilder builder,
            bool condition,
            DateTime value)
            => condition ? builder.Append(value.ToString(CultureInfo.CurrentCulture)) : builder;

        /// <summary>
        /// Appends a enum to this string builder when condition is true.
        /// </summary>
        public static StringBuilder Append(
            this StringBuilder builder,
            bool condition,
            Enum value)
            => condition ? builder.Append(value.ToString()) : builder;

        /// <summary>
        /// Appends values to this string builder with new line when condition is true.
        /// </summary>
        public static StringBuilder AppendLine(
            this StringBuilder builder,
            bool condition,
            string value)
            => condition ? builder.AppendLine(value) : builder;

        /// <summary>
        /// Appends a ushort to this string builder when condition is true.
        /// </summary>
        public static StringBuilder Append(
            this StringBuilder builder,
            bool condition,
            ushort value)
            => condition ? builder.Append(value) : builder;

        /// <summary>
        /// Appends an uint to this string builder when condition is true.
        /// </summary>
        public static StringBuilder Append(
            this StringBuilder builder,
            bool condition,
            uint value)
            => condition ? builder.Append(value) : builder;

        /// <summary>
        /// Appends an Object to this string builder when condition is true.
        /// </summary>
        public static StringBuilder Append(
            this StringBuilder builder,
            bool condition,
            object? value)
            => condition ? builder.Append(value) : builder;

        /// <summary>
        /// Appends values to this string builder.
        /// </summary>
        public static StringBuilder AppendJoin<T>(
            this StringBuilder stringBuilder,
            IEnumerable<T?> values,
            string separator = ", ")
            => stringBuilder.AppendJoin(values, (sb, value) => sb.Append(value), separator);

        /// <summary>
        /// Appends values to this string builder.
        /// </summary>
        public static StringBuilder AppendJoin<T>(
                    this StringBuilder stringBuilder,
                    IEnumerable<T?> values,
                    Action<StringBuilder, T?> joinAction,
                    string separator = ", ")
        {
            if (values is null)
            {
                return stringBuilder;
            }

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

        /// <summary>
        /// Appends values to this string builder.
        /// </summary>
        public static StringBuilder AppendJoin<T>(
                    this StringBuilder stringBuilder,
                    IEnumerable<T?> values,
                    Func<StringBuilder, T?, bool> joinFunc,
                    string separator = ", ")
        {
            if (values is null)
            {
                return stringBuilder;
            }

            var appended = false;

            foreach (var value in values)
            {
                if (joinFunc(stringBuilder, value))
                {
                    stringBuilder.Append(separator);
                    appended = true;
                }
            }

            if (appended)
            {
                stringBuilder.Length -= separator.Length;
            }

            return stringBuilder;
        }
    }
}
