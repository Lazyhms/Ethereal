// Copyright (c) Ethereal. All rights reserved.

using System.Collections.Generic;

namespace System.Linq
{
    /// <summary>
    /// LINQ related extension methods.
    /// </summary>
    public static class EnumerableExtensions
    {
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

        /// <summary>
        /// When the condition is true will use the predicate
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
        /// When the condition is true will use the trueExpression, otherwise use falseExpression
        /// </summary>
        public static IEnumerable<TSource> Where<TSource>(
            this IEnumerable<TSource> source,
            bool condition,
            (Func<TSource, bool> truePredicate, Func<TSource, bool> falsePredicate) predicate)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));

            return condition ? source.Where(predicate.truePredicate) : source.Where(predicate.falsePredicate);
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
        /// When the condition is true will use the trueExpression, otherwise use falseExpression
        /// </summary>
        public static IEnumerable<TSource> Where<TSource>(
            this IEnumerable<TSource> source,
            bool condition,
            (Func<TSource, int, bool> truePredicate, Func<TSource, int, bool> falsePredicate) predicate)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));

            return condition ? source.Where(predicate.truePredicate) : source.Where(predicate.falsePredicate);
        }

        /// <summary>
        /// Creates a<see cref="PagedList{T}" /> from an<see cref="IQueryable{T}" /> by enumerating it.
        /// </summary>
        public static PagedList<TSource> ToPagedList<TSource>(
            this IEnumerable<TSource> source,
            int pageIndex,
            int pageSize)
        {
            Check.NotNull(source, nameof(source));

            var count = source.Count();
            if (count == 0)
            {
                return PagedList.Empty<TSource>();
            }
            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            var pageCount = Convert.ToInt32(decimal.Ceiling(decimal.Divide(count, pageSize)));
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }
            var pageData = source.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            return new PagedList<TSource>
            {
                PageCount = pageCount,
                PageData = pageData,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = count
            };
        }

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

            return outer.GroupJoin(inner, outerKeySelector, innerKeySelector, (outer, inners) => new
            {
                outer,
                inners = inners.DefaultIfEmpty()
            }).SelectMany(joinResult => joinResult.inners.Select(innerObj => resultSelector(joinResult.outer, innerObj)));
        }

        /// <summary>
        /// Rank
        /// </summary>
        public static IEnumerable<(int rank, TSource)> Rank<TSource>(
            this IEnumerable<TSource> source) where TSource : struct
        {
            int index = 1, sequence = 1;
            TSource? lastest = default;
            using var itor = source.OrderByDescending(o => o).GetEnumerator();
            while (itor.MoveNext())
            {
                if (lastest is not null)
                {
                    if (!Equals(lastest ?? default, itor.Current))
                    {
                        index = index + sequence;
                        sequence = 1;
                    }
                    else
                    {
                        sequence++;
                    }
                }
                lastest = itor.Current;
                yield return (index, lastest!.Value);
            }
        }

        /// <summary>
        /// Rank
        /// </summary>
        public static IEnumerable<(int rank, TSource?)> Rank<TSource>(
            this IEnumerable<TSource?> source) where TSource : struct
        {
            int index = 1, sequence = 1;
            TSource? lastest = default;
            using var itor = source.OrderByDescending(o => o).GetEnumerator();
            while (itor.MoveNext())
            {
                if (lastest is not null)
                {
                    if (!Equals(lastest ?? default, itor.Current ?? default))
                    {
                        index = index + sequence;
                        sequence = 1;
                    }
                    else
                    {
                        sequence++;
                    }
                }
                lastest = itor.Current;
                yield return (index, lastest);
            }
        }

        /// <summary>
        /// Rank
        /// </summary>
        public static IEnumerable<(int rank, TSource)> Rank<TSource, TProperty>(
            this IEnumerable<TSource> source,
            Func<TSource, TProperty?> predicate) where TSource : class
                                                 where TProperty : struct
        {
            int index = 1, sequence = 1;
            TSource? lastest = default;
            using var itor = source.OrderByDescending(predicate).GetEnumerator();
            while (itor.MoveNext())
            {
                if (lastest is not null)
                {
                    if (!Equals(predicate(lastest!) ?? default, predicate(itor.Current) ?? default))
                    {
                        index = index + sequence;
                        sequence = 1;
                    }
                    else
                    {
                        sequence++;
                    }
                }
                lastest = itor.Current;
                yield return (index, lastest);
            }
        }

        /// <summary>
        /// ToTreeNode
        /// </summary>
        public static IEnumerable<TSource> ToTreeNode<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TKey> relationKeySelector,
            TKey rootValue) where TSource : TreeNode<TSource>
                            where TKey : notnull
        {
            var dic = source.ToDictionary(keySelector, k => k);

            foreach (var item in dic.Values)
            {
                if (dic.ContainsKey(relationKeySelector(item)))
                {
                    dic[relationKeySelector(item)].Children.Add(item);
                }
            }

            return dic.Values.Where(p => Equals(relationKeySelector(p), rootValue));
        }
    }
}