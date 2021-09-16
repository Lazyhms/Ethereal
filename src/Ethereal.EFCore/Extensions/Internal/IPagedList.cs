﻿// Copyright (c) Ethereal. All rights reserved.

using System;
using System.Collections.Generic;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// IPagedList
    /// </summary>
    public interface IPagedList<TEntity>
    {
        /// <summary>
        /// FirstRowOnPage
        /// </summary>
        int FirstRowOnPage => TotalCount == 0 ? 0 : (PageIndex - 1) * PageSize + 1;

        /// <summary>
        /// LastRowOnPage
        /// </summary>
        int LastRowOnPage => Math.Min(PageIndex * PageSize, TotalCount);

        /// <summary>
        /// PageCount
        /// </summary>
        int PageCount { get; set; }

        /// <summary>
        /// Page data
        /// </summary>
        IEnumerable<TEntity> PageData { get; set; }

        /// <summary>
        /// Index of page
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// PageSize
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// TotalCount
        /// </summary>
        int TotalCount { get; set; }
    }
}