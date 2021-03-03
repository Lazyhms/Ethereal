// Copyright (c) Ethereal. All rights reserved.
//

using System.Collections.Generic;
using System.IO;

namespace Ethereal.Excel
{
    /// <summary>
    /// IExcelRender
    /// </summary>
    public interface IExcelRender
    {
        /// <summary>
        /// RenderFromExcel
        /// </summary>
        IEnumerable<T>? RenderFromExcel<T>(Stream stream);

        /// <summary>
        /// RenderFromExcel
        /// </summary>
        IEnumerable<T>? RenderFromExcel<T>(FileInfo fileInfo);

        /// <summary>
        /// RenderToExcel
        /// </summary>
        Stream? RenderToExcel<T>(IEnumerable<T> source);
    }
}
