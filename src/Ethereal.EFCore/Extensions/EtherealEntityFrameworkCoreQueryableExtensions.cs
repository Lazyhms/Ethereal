// Copyright (c) Ethereal. All rights reserved.

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// Ethereal Entity Framework LINQ related extension methods.
    /// </summary>
    public static class EtherealEntityFrameworkCoreQueryableExtensions
    {

        private static readonly MethodInfo OrderByMethodInfo
            = typeof(Queryable).GetTypeInfo().GetDeclaredMethods(nameof(Queryable.OrderBy))
                .Single(mi => mi.GetGenericArguments().Count() == 2 && mi.GetParameters().Count() == 2);
        private static readonly MethodInfo OrderByDescendingMethodInfo
            = typeof(Queryable).GetTypeInfo().GetDeclaredMethods(nameof(Queryable.OrderByDescending))
                .Single(mi => mi.GetGenericArguments().Count() == 2 && mi.GetParameters().Count() == 2);

        /// <summary>
        /// creates a<see cref="PagedList{T}" /> from an<see cref="IQueryable{T}" /> by enumerating it.
        /// </summary>
        public static PagedList<TSource> ToPagedList<TSource>(
            this IQueryable<TSource> source,
            int pageIndex,
            int pageSize) where TSource : class
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
            pageIndex = pageIndex < 1 ? 1 : pageIndex;

            if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }
            var pageData = source.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            return new PagedList<TSource>
            {
                PageCount = pageCount,
                PagedData = pageData,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = count
            };
        }

        /// <summary>
        /// Asynchronously creates a <see cref="PagedList{T}" /> from an <see cref="IQueryable{T}" /> by enumerating it asynchronously.
        /// </summary>
        public static async Task<PagedList<TSource>> ToPagedListAsync<TSource>(
           this IQueryable<TSource> source,
           int pageIndex,
           int pageSize,
           CancellationToken cancellationToken = default) where TSource : class
        {
            Check.NotNull(source, nameof(source));

            var count = await source.CountAsync(cancellationToken);
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
            var pageData = await source.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync(cancellationToken);
            return new PagedList<TSource>
            {
                PageCount = pageCount,
                PagedData = pageData,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = count
            };
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a string key.
        /// </summary>
        public static IQueryable<TSource> OrderBy<TSource>(
            this IQueryable<TSource> source,
            string name)
        {
            var parameter = Expression.Parameter(typeof(TSource));
            var keySelector = Expression.PropertyOrField(parameter, name);

            return source.Provider.CreateQuery<TSource>(
                    Expression.Call(null,
                    OrderByMethodInfo.MakeGenericMethod(parameter.Type, keySelector.Type),
                    new[] { source.Expression, Expression.Lambda(keySelector, parameter) }));
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a string key.
        /// </summary>
        public static IQueryable<TSource> OrderByDescending<TSource>(
            this IQueryable<TSource> source,
            string name)
        {
            var parameter = Expression.Parameter(typeof(TSource));
            var keySelector = Expression.PropertyOrField(parameter, name);

            return source.Provider.CreateQuery<TSource>(
                    Expression.Call(null,
                    OrderByDescendingMethodInfo.MakeGenericMethod(parameter.Type, keySelector.Type),
                    new[] { source.Expression, Expression.Lambda(keySelector, parameter) }));
        }

        /// <summary>
        /// When the condition is true will use the predicate
        /// </summary>
        public static IQueryable<TSource> Where<TSource>(
            this IQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, bool>> predicate)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));

            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// When the condition is true will use the predicate
        /// </summary>
        /// 
        public static IQueryable<TSource> Where<TSource>(
            this IQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, int, bool>> predicate)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));

            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// When the condition is true will use the trueExpression, otherwise use falseExpression
        /// </summary>
        public static IQueryable<TSource> WhereIf<TSource>(
            this IQueryable<TSource> source,
            bool condition,
            (Expression<Func<TSource, bool>> trueExpression, Expression<Func<TSource, bool>> falseExpression) predicate)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));

            return condition ? source.Where(predicate.trueExpression) : source.Where(predicate.falseExpression);
        }

        /// <summary>
        /// When the condition is true will use the trueExpression, otherwise use falseExpression
        /// </summary>
        public static IQueryable<TSource> WhereIf<TSource>(
            this IQueryable<TSource> source,
            bool condition,
            (Expression<Func<TSource, int, bool>> trueExpression, Expression<Func<TSource, int, bool>> falseExpression) predicate)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));

            return condition ? source.Where(predicate.trueExpression) : source.Where(predicate.falseExpression);
        }

        /// <summary>
        /// Left join
        /// </summary>
        public static IQueryable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
            this IQueryable<TOuter> outer,
            IQueryable<TInner> inner,
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector,
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
                inners
            }).SelectMany(collectionSelector => collectionSelector.inners.DefaultIfEmpty(), (r, s) => resultSelector(r.outer, s));
        }
    }
}