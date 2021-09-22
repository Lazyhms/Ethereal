// Copyright (c) Ethereal. All rights reserved.

using Ethereal.NETCore;
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
            this IEnumerable<TSource> source,
            TSource item,
            Func<TSource?, TSource?, bool> selector) where TSource : class
        {
            Check.NotNull(source, nameof(source));

            return source.Contains(item, new DynamicEqualityComparer<TSource>(selector));
        }

        /// <summary>
        /// Contains
        /// </summary>
        public static bool Contains<TSource, TKey>(
            this IEnumerable<TSource> source,
            TSource item,
            Func<TSource?, TKey?> selector) where TSource : class
        {
            Check.NotNull(source, nameof(source));

            return source.Contains(item, new DynamicEqualityComparer<TSource, TKey>(selector));
        }

        /// <summary>
        /// Distinct
        /// </summary>
        public static IEnumerable<TSource> Distinct<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource?, TSource?, bool> selector) where TSource : class
        {
            Check.NotNull(source, nameof(source));

            return source.Distinct(new DynamicEqualityComparer<TSource>(selector));
        }

        /// <summary>
        /// Distinct
        /// </summary>
        public static IEnumerable<TSource> Distinct<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource?, TKey?> selector) where TSource : class
        {
            Check.NotNull(source, nameof(source));

            return source.Distinct(new DynamicEqualityComparer<TSource, TKey>(selector));
        }

        /// <summary>
        /// Except
        /// </summary>
        public static IEnumerable<TSource> Except<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            Func<TSource?, TSource?, bool> selector) where TSource : class
        {
            Check.NotNull(first, nameof(first));

            return first.Except(second, new DynamicEqualityComparer<TSource>(selector));
        }

        /// <summary>
        /// Except
        /// </summary>
        public static IEnumerable<TSource> Except<TSource, TKey>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            Func<TSource?, TKey?> selector) where TSource : class
        {
            Check.NotNull(first, nameof(first));

            return first.Except(second, new DynamicEqualityComparer<TSource, TKey>(selector));
        }

        /// <summary>
        /// Intersect
        /// </summary>
        public static IEnumerable<TSource> Intersect<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            Func<TSource?, TSource?, bool> selector) where TSource : class
        {
            Check.NotNull(first, nameof(first));

            return first.Intersect(second, new DynamicEqualityComparer<TSource>(selector));
        }

        /// <summary>
        /// Intersect
        /// </summary>
        public static IEnumerable<TSource> Intersect<TSource, TKey>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            Func<TSource?, TKey?> selector) where TSource : class
        {
            Check.NotNull(first, nameof(first));

            return first.Intersect(second, new DynamicEqualityComparer<TSource, TKey>(selector));
        }

        /// <summary>
        /// Union
        /// </summary>
        public static IEnumerable<TSource> Union<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            Func<TSource?, TSource?, bool> selector) where TSource : class
        {
            Check.NotNull(first, nameof(first));

            return first.Union(second, new DynamicEqualityComparer<TSource>(selector));
        }

        /// <summary>
        /// Union
        /// </summary>
        public static IEnumerable<TSource> Union<TSource, TKey>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            Func<TSource?, TKey?> selector) where TSource : class
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
            this IEnumerable<TSource> source,
            bool condition,
            Func<TSource, bool> predicate)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));

            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// when the condition is true will use the predicate
        /// </summary>
        public static IEnumerable<TSource> Where<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate,
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
            this IEnumerable<TSource> source,
            bool condition,
            Func<TSource, bool> truePredicate,
            Func<TSource, bool> falsePredicate)
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
            this IEnumerable<TSource> source,
            bool condition,
            Func<TSource, int, bool> predicate)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));

            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// when the condition is true will use the predicate
        /// </summary>
        public static IEnumerable<TSource> Where<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, int, bool> predicate,
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
            this IEnumerable<TSource> source,
            bool condition,
            Func<TSource, int, bool> truePredicate,
            Func<TSource, int, bool> falsePredicate)
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
            this IEnumerable<TEntity> source,
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
            var pageData = source.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
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
            this IEnumerable<TEntity> source,
            Func<TEntity, TKey> keySelector,
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
            this IEnumerable<TEntity> source,
            Func<TEntity, bool> predicate,
            Func<TEntity, TKey> keySelector,
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
            this IEnumerable<TEntity> source,
            Func<TEntity, TKey> selector,
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
            this IEnumerable<TEntity> source,
            Func<TEntity, bool> predicate,
            Func<TEntity, TKey> keySelector,
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
            this IEnumerable<TSource> source,
            int index,
            TSource present)
        {
            Check.NotNull(source, nameof(source));

            var i = -1;
            foreach (var element in source)
            {
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
            this IEnumerable<TSource> source,
            TSource original,
            TSource present)
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
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            TSource present)
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
            TSource original,
            TSource present,
            Func<TSource, TKey> compare) where TSource : class
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
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner?, TResult> resultSelector)
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
           this IEnumerable<TSource> source,
           char separator = ',')
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(separator, nameof(separator));

            return string.Join(separator, source);
        }

        /// <summary>
        /// Join
        /// </summary>
        public static string Join<TSource>(
           this IEnumerable<TSource> source,
           string? separator = ",")
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(separator, nameof(separator));

            return string.Join(separator, source);
        }

        #endregion Join

        /// <summary>
        /// Rank
        /// </summary>
        public static IEnumerable<(int rank, TSource)> Rank<TSource>(this IEnumerable<TSource> sources)
        {
            var rank = new List<(int, TSource)>();
            int index = 1, sequence = 1;
            foreach (var item in sources)
            {
                if (rank.Any())
                {
                    var lastest = rank.Last();
                    if (!Equals(item, lastest.Item2))
                    {
                        index = lastest.Item1 + sequence;
                        sequence = 1;
                    }
                    else
                    {
                        sequence++;
                    }
                }
                rank.Add((index, item));
            }
            return rank;
        }

        /// <summary>
        /// Rank
        /// </summary>
        public static IEnumerable<(int rank, TSource)> Rank<TSource, TProperty>(
            this IEnumerable<TSource> sources,
            Func<TSource, TProperty?> predicate) where TSource : class
                                                 where TProperty : struct
        {
            var rank = new List<(int, TSource)>();
            int index = 1, sequence = 1;
            var rankSource = sources.Where(w => w is not null).OrderByDescending(predicate);
            foreach (var item in rankSource)
            {
                if (rank.Any())
                {
                    var lastest = rank.Last();
                    if (!Equals(predicate(item!), predicate(lastest.Item2)))
                    {
                        index = lastest.Item1 + sequence;
                        sequence = 1;
                    }
                    else
                    {
                        sequence++;
                    }
                }
                rank.Add((index, item));
            }
            return rank;
        }
    }
}