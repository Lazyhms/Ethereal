// Copyright (c) Ethereal. All rights reserved.

using System.Collections.Generic;

namespace System.Linq
{
    /// <summary>
    /// PagedList
    /// </summary>
    public class PagedList
    {
        /// <summary>
        /// Empty PagedList
        /// </summary>
        public static IPagedList<T> Empty<T>() => EmptyPagedList<T>.Value;

        private class EmptyPagedList<T>
        {
            public static readonly IPagedList<T> Value = new PagedList<T>()
            {
                PageIndex = 1,
                PageSize = 0,
                PageCount = 1,
                TotalCount = 0
            };
        }
    }

    /// <summary>
    /// Query PagedList
    /// </summary>
    public class PagedList<TEntity> : IPagedList<TEntity>
    {
        /// <inheritdoc/>
        public int PageCount { get; set; }

        /// <inheritdoc/>
        public IEnumerable<TEntity> PageData { get; set; } = Array.Empty<TEntity>();

        /// <inheritdoc/>
        public int PageIndex { get; set; }

        /// <inheritdoc/>
        public int PageSize { get; set; }

        /// <inheritdoc/>
        public int TotalCount { get; set; }
    }
}