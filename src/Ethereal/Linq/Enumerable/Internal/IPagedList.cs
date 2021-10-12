// Copyright (c) Ethereal. All rights reserved.

using System.Collections.Generic;

namespace System.Linq
{
    /// <summary>
    /// IPagedList
    /// </summary>
    public interface IPagedList<TEntity>
    {
        /// <summary>
        /// Index of page
        /// </summary>
        int PageIndex { get; set; }
        /// <summary>
        /// PageSize
        /// </summary>
        int PageSize { get; set; }
        /// <summary>
        /// PageCount
        /// </summary>
        int PageCount { get; set; }
        /// <summary>
        /// TotalCount
        /// </summary>
        int TotalCount { get; set; }
        /// <summary>
        /// Page data
        /// </summary>
        IEnumerable<TEntity> PageData { get; set; }
    }
}