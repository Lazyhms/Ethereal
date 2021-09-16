// Copyright (c) Ethereal. All rights reserved.

using Ethereal.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// LINQ related extension methods.
    /// </summary>
    public static class EtherealEFCoreQueryableExtensions
    {
        #region Pagination

        /// <summary>
        /// Pagination
        /// </summary>
        public static IPagedList<TEntity> Pagination<TEntity>(
            this IQueryable<TEntity> source,
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
        /// PaginationAsync
        /// </summary>
        public static async Task<IPagedList<TEntity>> PaginationAsync<TEntity>(
           this IQueryable<TEntity> source,
           int pageIndex,
           int pageSize,
           CancellationToken cancellationToken = default) where TEntity : class
        {
            Check.NotNull(source, nameof(source));

            var count = await source.CountAsync(cancellationToken);
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
            var pageData = await source.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync(cancellationToken);
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
            this IQueryable<TEntity> source,
            Expression<Func<TEntity, TKey>> keySelector,
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
            this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TKey>> keySelector,
            int pageIndex,
            int pageSize) where TEntity : class
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));
            Check.NotNull(keySelector, nameof(keySelector));

            return source.Where(predicate).OrderBy(keySelector).Pagination(pageIndex, pageSize);
        }

        /// <summary>
        /// PaginationAsync
        /// </summary>
        public static async Task<IPagedList<TEntity>> PaginationByAsync<TEntity, TKey>(
            this IQueryable<TEntity> source,
            Expression<Func<TEntity, TKey>> keySelector,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default) where TEntity : class
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(keySelector, nameof(keySelector));

            return await source.OrderBy(keySelector).PaginationAsync(pageIndex, pageSize, cancellationToken);
        }

        /// <summary>
        /// PaginationAsync
        /// </summary>
        public static async Task<IPagedList<TEntity>> PaginationByAsync<TEntity, TKey>(
            this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TKey>> keySelector,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default) where TEntity : class
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));
            Check.NotNull(keySelector, nameof(keySelector));

            return await source.Where(predicate).OrderBy(keySelector).PaginationAsync(pageIndex, pageSize, cancellationToken);
        }

        /// <summary>
        /// PagedList
        /// </summary>
        public static IPagedList<TEntity> PaginationByDescending<TEntity, TKey>(
            this IQueryable<TEntity> source,
            Expression<Func<TEntity, TKey>> keySelector,
            int pageIndex,
            int pageSize) where TEntity : class
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(keySelector, nameof(keySelector));

            return source.OrderByDescending(keySelector).Pagination(pageIndex, pageSize);
        }

        /// <summary>
        /// PagedList
        /// </summary>
        public static IPagedList<TEntity> PaginationByDescending<TEntity, TKey>(
            this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TKey>> keySelector,
            int pageIndex,
            int pageSize) where TEntity : class
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));
            Check.NotNull(keySelector, nameof(keySelector));

            return source.Where(predicate).OrderByDescending(keySelector).Pagination(pageIndex, pageSize);
        }

        /// <summary>
        /// PaginationAsync
        /// </summary>
        public static async Task<IPagedList<TEntity>> PaginationByDescendingAsync<TEntity, TKey>(
            this IQueryable<TEntity> source,
            Expression<Func<TEntity, TKey>> keySelector,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default) where TEntity : class
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(keySelector, nameof(keySelector));

            return await source.OrderByDescending(keySelector).PaginationAsync(pageIndex, pageSize, cancellationToken);
        }

        /// <summary>
        /// PaginationAsync
        /// </summary>
        public static async Task<IPagedList<TEntity>> PaginationByDescendingAsync<TEntity, TKey>(
            this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TKey>> keySelector,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default) where TEntity : class
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));
            Check.NotNull(keySelector, nameof(keySelector));

            return await source.Where(predicate).OrderByDescending(keySelector).PaginationAsync(pageIndex, pageSize, cancellationToken);
        }

        #endregion Pagination

        #region Where

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
        public static IQueryable<TSource> Where<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            bool condition)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));

            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// when the condition is true will use the predicate
        /// </summary>
        public static IQueryable<TSource> Where<TSource>(
            this IQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, bool>> truePredicate,
            Expression<Func<TSource, bool>> falsePredicate)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(truePredicate, nameof(truePredicate));
            Check.NotNull(falsePredicate, nameof(falsePredicate));

            return condition ? source.Where(truePredicate) : source.Where(falsePredicate);
        }

        /// <summary>
        /// When the condition is true will use the predicate
        /// </summary>
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
        /// When the condition is true will use the predicate
        /// </summary>
        public static IQueryable<TSource> Where<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, int, bool>> predicate,
            bool condition)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));

            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// when the condition is true will use the predicate
        /// </summary>
        public static IQueryable<TSource> Where<TSource>(
            this IQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, int, bool>> truePredicate,
            Expression<Func<TSource, int, bool>> falsePredicate)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(truePredicate, nameof(truePredicate));
            Check.NotNull(falsePredicate, nameof(falsePredicate));

            return condition ? source.Where(truePredicate) : source.Where(falsePredicate);
        }

        #endregion Where
    }
}