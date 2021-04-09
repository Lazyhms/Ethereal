// Copyright (c) Ethereal. All rights reserved.

using Ethereal.NETCore;
using Ethereal.Utilities;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace System.Linq
{
    /// <summary>
    /// LINQ related extension methods.
    /// </summary>
    public static partial class EtherealEnumerableExtensions
    {
        #region Distinct,Union,Except,Intersect,Contains

        /// <summary>
        /// Contains
        /// </summary>
        public static bool Contains<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [CanBeNull] TSource item,
            [NotNull] Func<TSource?, TSource?, bool> selector) where TSource : class
        {
            Check.NotNull(source, nameof(source));

            return source.Contains(item, new DynamicEqualityComparer<TSource>(selector));
        }

        /// <summary>
        /// Contains
        /// </summary>
        public static bool Contains<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [CanBeNull] TSource item,
            [NotNull] Func<TSource?, TKey?> selector) where TSource : class
        {
            Check.NotNull(source, nameof(source));

            return source.Contains(item, new DynamicEqualityComparer<TSource, TKey>(selector));
        }

        /// <summary>
        /// Distinct
        /// </summary>
        public static IEnumerable<TSource> Distinct<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource?, TSource?, bool> selector) where TSource : class
        {
            Check.NotNull(source, nameof(source));

            return source.Distinct(new DynamicEqualityComparer<TSource>(selector));
        }

        /// <summary>
        /// Distinct
        /// </summary>
        public static IEnumerable<TSource> Distinct<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource?, TKey?> selector) where TSource : class
        {
            Check.NotNull(source, nameof(source));

            return source.Distinct(new DynamicEqualityComparer<TSource, TKey>(selector));
        }

        /// <summary>
        /// Except
        /// </summary>
        public static IEnumerable<TSource> Except<TSource>(
            [NotNull] this IEnumerable<TSource> first,
            [CanBeNull] IEnumerable<TSource> second,
            [NotNull] Func<TSource?, TSource?, bool> selector) where TSource : class
        {
            Check.NotNull(first, nameof(first));

            return first.Except(second, new DynamicEqualityComparer<TSource>(selector));
        }

        /// <summary>
        /// Except
        /// </summary>
        public static IEnumerable<TSource> Except<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> first,
            [CanBeNull] IEnumerable<TSource> second,
            [NotNull] Func<TSource?, TKey?> selector) where TSource : class
        {
            Check.NotNull(first, nameof(first));

            return first.Except(second, new DynamicEqualityComparer<TSource, TKey>(selector));
        }

        /// <summary>
        /// Intersect
        /// </summary>
        public static IEnumerable<TSource> Intersect<TSource>(
            [NotNull] this IEnumerable<TSource> first,
            [CanBeNull] IEnumerable<TSource> second,
            [NotNull] Func<TSource?, TSource?, bool> selector) where TSource : class
        {
            Check.NotNull(first, nameof(first));

            return first.Intersect(second, new DynamicEqualityComparer<TSource>(selector));
        }

        /// <summary>
        /// Intersect
        /// </summary>
        public static IEnumerable<TSource> Intersect<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> first,
            [CanBeNull] IEnumerable<TSource> second,
            [NotNull] Func<TSource?, TKey?> selector) where TSource : class
        {
            Check.NotNull(first, nameof(first));

            return first.Intersect(second, new DynamicEqualityComparer<TSource, TKey>(selector));
        }

        /// <summary>
        /// Union
        /// </summary>
        public static IEnumerable<TSource> Union<TSource>(
            [NotNull] this IEnumerable<TSource> first,
            [CanBeNull] IEnumerable<TSource> second,
            [NotNull] Func<TSource?, TSource?, bool> selector) where TSource : class
        {
            Check.NotNull(first, nameof(first));

            return first.Union(second, new DynamicEqualityComparer<TSource>(selector));
        }

        /// <summary>
        /// Union
        /// </summary>
        public static IEnumerable<TSource> Union<TSource, TKey>(
            [NotNull] this IEnumerable<TSource> first,
            [CanBeNull] IEnumerable<TSource> second,
            [NotNull] Func<TSource?, TKey?> selector) where TSource : class
        {
            Check.NotNull(first, nameof(first));

            return first.Union(second, new DynamicEqualityComparer<TSource, TKey>(selector));
        }

        private sealed class DynamicEqualityComparer<T> : IEqualityComparer<T> where T : class
        {
            private readonly Func<T?, T?, bool> _func;

            public DynamicEqualityComparer(Func<T?, T?, bool> func) => _func = func;

            public bool Equals(T? x, T? y) => _func(x, y);

            public int GetHashCode(T? obj) => 0;
        }

        private sealed class DynamicEqualityComparer<TS, TK> : IEqualityComparer<TS> where TS : class
        {
            private readonly Func<TS?, TK?> _func;

            public DynamicEqualityComparer(Func<TS?, TK?> func) => _func = func;

            public bool Equals(TS? x, TS? y) => Equals(_func(x), _func(y));

            public int GetHashCode(TS? obj) => 0;
        }

        #endregion Distinct,Union,Except,Intersect,Contains

        #region Where

        /// <summary>
        /// when the condition is true will use the predicate
        /// </summary>
        public static IEnumerable<TSource> Where<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            bool condition,
            [NotNull] Func<TSource, bool> predicate)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));

            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// when the condition is true will use the predicate
        /// </summary>
        public static IEnumerable<TSource> Where<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, bool> predicate,
            bool condition)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));

            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// when the condition is true will use the predicate
        /// </summary>
        public static IEnumerable<TSource> Where<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            bool condition,
            [NotNull] Func<TSource, bool> truePredicate,
            [NotNull] Func<TSource, bool> falsePredicate)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(truePredicate, nameof(truePredicate));
            Check.NotNull(falsePredicate, nameof(falsePredicate));

            return condition ? source.Where(truePredicate) : source.Where(falsePredicate);
        }

        /// <summary>
        /// when the condition is true will use the predicate
        /// </summary>
        public static IEnumerable<TSource> Where<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            bool condition,
            [NotNull] Func<TSource, int, bool> predicate)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));

            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// when the condition is true will use the predicate
        /// </summary>
        public static IEnumerable<TSource> Where<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, int, bool> predicate,
            bool condition)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));

            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// when the condition is true will use the predicate
        /// </summary>
        public static IEnumerable<TSource> Where<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            bool condition,
            [NotNull] Func<TSource, int, bool> truePredicate,
            [NotNull] Func<TSource, int, bool> falsePredicate)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(truePredicate, nameof(truePredicate));
            Check.NotNull(falsePredicate, nameof(falsePredicate));

            return condition ? source.Where(truePredicate) : source.Where(falsePredicate);
        }

        #endregion Where

        #region Pagination

        /// <summary>
        /// Pagination
        /// </summary>
        public static IPagedList<TEntity> Pagination<TEntity>(
            [NotNull] this IEnumerable<TEntity> source,
            int pageIndex,
            int pageSize) where TEntity : class
        {
            Check.NotNull(source, nameof(source));

            var count = source.Count();
            if (count == 0)
            {
                return PagedList.Empty<TEntity>();
            }
            if (pageSize <= 0)
            {
                throw new ArgumentException(CoreStrings.PageSize_Invalid);
            }
            var pageCount = (int)decimal.Ceiling(decimal.Divide(count, pageSize));
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }
            var pageData = source.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            return new PagedList<TEntity>
            {
                PageCount = pageCount,
                PageData = pageData,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = count
            };
        }

        /// <summary>
        /// PagedList
        /// </summary>
        public static IPagedList<TEntity> PaginationBy<TEntity, TKey>(
            [NotNull] this IEnumerable<TEntity> source,
            [NotNull] Func<TEntity, TKey> keySelector,
            int pageIndex,
            int pageSize) where TEntity : class
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(keySelector, nameof(keySelector));

            return source.OrderBy(keySelector).Pagination(pageIndex, pageSize);
        }

        /// <summary>
        /// PagedList
        /// </summary>
        public static IPagedList<TEntity> PaginationBy<TEntity, TKey>(
            [NotNull] this IEnumerable<TEntity> source,
            [NotNull] Func<TEntity, bool> predicate,
            [NotNull] Func<TEntity, TKey> keySelector,
            int pageIndex,
            int pageSize) where TEntity : class
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));
            Check.NotNull(keySelector, nameof(keySelector));

            return source.Where(predicate).OrderBy(keySelector).Pagination(pageIndex, pageSize);
        }

        /// <summary>
        /// PagedList
        /// </summary>
        public static IPagedList<TEntity> PaginationByByDescending<TEntity, TKey>(
            [NotNull] this IEnumerable<TEntity> source,
            [NotNull] Func<TEntity, TKey> selector,
            int pageIndex,
            int pageSize) where TEntity : class
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(selector, nameof(selector));

            return source.OrderByDescending(selector).Pagination(pageIndex, pageSize);
        }

        /// <summary>
        /// PagedList
        /// </summary>
        public static IPagedList<TEntity> PaginationByByDescending<TEntity, TKey>(
            [NotNull] this IEnumerable<TEntity> source,
            [NotNull] Func<TEntity, bool> predicate,
            [NotNull] Func<TEntity, TKey> keySelector,
            int pageIndex,
            int pageSize) where TEntity : class
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));
            Check.NotNull(keySelector, nameof(keySelector));

            return source.Where(predicate).OrderByDescending(keySelector).Pagination(pageIndex, pageSize);
        }

        #endregion Pagination

        #region Replace

        /// <summary>
        /// Replace
        /// </summary>
        public static IEnumerable<TSource> Replace<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            int index,
            [CanBeNull] TSource present)
        {
            Check.NotNull(source, nameof(source));

            var i = -1;
            foreach (var element in source)
            {
                checked { i++; }
                if (i == index)
                {
                    yield return present;
                }
                else
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// ReplaceAll
        /// </summary>
        public static IEnumerable<TSource> ReplaceAll<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [CanBeNull] TSource original,
            [CanBeNull] TSource present)
        {
            Check.NotNull(source, nameof(source));

            foreach (var element in source)
            {
                if (Equals(element, original))
                {
                    yield return present;
                }
                else
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// ReplaceAll
        /// </summary>
        public static IEnumerable<TSource> ReplaceAll<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, bool> predicate,
            [CanBeNull] TSource present)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));

            foreach (var element in source)
            {
                if (predicate(element))
                {
                    yield return present;
                }
                else
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// ReplaceAll
        /// </summary>
        public static IEnumerable<TSource> ReplaceAll<TSource, TKey>(
            this IEnumerable<TSource> source,
            [CanBeNull] TSource original,
            [CanBeNull] TSource present,
            [NotNull] Func<TSource, TKey> compare) where TSource : class
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(compare, nameof(compare));

            foreach (var element in source)
            {
                if (Equals(compare(element), compare(original)))
                {
                    yield return present;
                }
                else
                {
                    yield return element;
                }
            }
        }

        #endregion Replace

        #region LeftJoin

        /// <summary>
        /// LeftJoin
        /// </summary>
        public static IEnumerable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
            [NotNull] this IEnumerable<TOuter> outer,
            [NotNull] IEnumerable<TInner> inner,
            [NotNull] Func<TOuter, TKey> outerKeySelector,
            [NotNull] Func<TInner, TKey> innerKeySelector,
            [NotNull] Func<TOuter, TInner?, TResult> resultSelector)
        {
            Check.NotNull(outer, nameof(outer));
            Check.NotNull(inner, nameof(inner));
            Check.NotNull(outerKeySelector, nameof(outerKeySelector));
            Check.NotNull(innerKeySelector, nameof(innerKeySelector));
            Check.NotNull(resultSelector, nameof(resultSelector));

            return outer.GroupJoin(inner, outerKeySelector, innerKeySelector, (outerObj, inners) => new
            {
                outerObj,
                inners = inners.DefaultIfEmpty()
            }).SelectMany(joinResult => joinResult.inners.Select(innerObj => resultSelector(joinResult.outerObj, innerObj)));
        }

        #endregion LeftJoin

        #region Join

        /// <summary>
        /// Join
        /// </summary>
        public static string Join<TSource>(
           [NotNull] this IEnumerable<TSource> source,
           [NotNull] char separator = ',')
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(separator, nameof(separator));

            return string.Join(separator, source);
        }

        /// <summary>
        /// Join
        /// </summary>
        public static string Join<TSource>(
           [NotNull] this IEnumerable<TSource> source,
           [NotNull] string? separator = ",")
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(separator, nameof(separator));

            return string.Join(separator, source);
        }

        #endregion Join
    }
}