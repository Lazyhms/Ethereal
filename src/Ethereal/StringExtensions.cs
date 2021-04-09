// Copyright (c) Ethereal. All rights reserved.

using JetBrains.Annotations;
using System.Linq;

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
            [CanBeNull] this string source,
            [CanBeNull] string compare)
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
    }
}