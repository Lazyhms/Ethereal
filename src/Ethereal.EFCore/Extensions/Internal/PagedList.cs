// Copyright (c) Ethereal. All rights reserved.
//

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Ethereal.EntityFrameworkCore
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

        /// <summary>
        /// override ToString method
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string? ToString() => base.ToString();

        /// <summary>
        /// override Equals method
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object? obj) => base.Equals(obj);

        /// <summary>
        /// override GetHashCode method
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => base.GetHashCode();
    }

    /// <summary>
    /// Query PagedList
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class PagedList<TEntity> : IPagedList<TEntity>
    {
        /// <inheritdoc/>
        public int PageIndex { get; set; }

        /// <inheritdoc/>
        public int PageSize { get; set; }

        /// <inheritdoc/>
        public int PageCount { get; set; }

        /// <inheritdoc/>
        public int TotalCount { get; set; }

        /// <inheritdoc/>
        public IList<TEntity> PageData { get; set; } = Array.Empty<TEntity>();
    }
}