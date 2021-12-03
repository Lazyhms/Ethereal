// Copyright (c) Ethereal. All rights reserved.

using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// SharedExtensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Compare the similarity of two strings
        /// </summary>
        public static decimal Similarity(
            this string source,
            string compare)
        {
            if (source is null && compare is null)
            {
                return 1;
            }
            if (source is null || compare is null)
            {
                return 0;
            }
            var intersect = source!.Intersect(compare!).Count();
            if (intersect == 0)
            {
                return 0;
            }

            var union = source!.Union(compare!).Count();
            if (union == 0)
            {
                return 0;
            }

            return decimal.Divide(intersect, union);
        }

        /// <summary>
        /// Indicates whether the specified string is null or an empty string ("").
        /// </summary>
        public static string IsNullOrEmpty(
            this string source,
            string compare)
            => string.IsNullOrEmpty(source) ? compare : source;

        /// <summary>
        /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        public static string IsNullOrWhiteSpace(
            this string source,
            string compare)
            => string.IsNullOrWhiteSpace(source) ? compare : source;

        /// <summary>
        /// Desensitize a string of characters
        /// </summary>
        public static string Desensitize(
            this string source,
            int startLength = 3,
            int endLength = 4)
        {
            var sourceSpan = source.AsSpan();
            var leftSpan = sourceSpan.Slice(0, startLength);
            Span<char> centerSpan;
            {
                var centerLength = source.Length - startLength - endLength;
                centerSpan = new Span<char>(new char[centerLength]);
                centerSpan.Fill('*');
            }
            var rightSpan = sourceSpan.Slice(source.Length - endLength);

            return new StringBuilder(source.Length)
                .Append(leftSpan)
                .Append(centerSpan)
                .Append(rightSpan)
                .ToString();
        }
    }
}