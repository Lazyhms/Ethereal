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
        public static PagedList<T> Empty<T>() => EmptyPagedList<T>.Value;

        private class EmptyPagedList<T>
        {
            public static readonly PagedList<T> Value = new PagedList<T>()
            {
                PageIndex = 1,
                PageSize = 10,
                PageCount = 1,
                TotalCount = 0
            };
        }
    }

    /// <summary>
    /// Query PagedList
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
        public IEnumerable<TEntity> PageData { get; set; } = Array.Empty<TEntity>();
    }
}