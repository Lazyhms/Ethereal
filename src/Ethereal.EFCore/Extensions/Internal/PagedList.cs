// Copyright (c) Ethereal. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// PagedList
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class PagedList
    {
        /// <summary>
        /// Empty PagedList
        /// </summary>
        public static PagedList<T> Empty<T>() => EmptyPagedList<T>.Value;

        private class EmptyPagedList<T>
        {
            public static readonly PagedList<T> Value = new PagedList<T>()
            {
                PageIndex = 1,
                PageSize = 0,
                PageCount = 1,
                TotalCount = 0
            };
        }
    }

    /// <summary>
    /// Paged List
    /// </summary>
    public class PagedList<TEntity>
    {
        /// <summary>
        /// Index of page
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// PageSize
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// PageCount
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// TotalCount
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// Page data
        /// </summary>
        public IList<TEntity> PagedData { get; set; } = Array.Empty<TEntity>();
    }
}