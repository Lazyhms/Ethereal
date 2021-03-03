// Copyright (c) Ethereal. All rights reserved.
//

using System;

namespace Ethereal.Excel.Abstractions
{
    /// <summary>
    /// ExcelTopHeaderAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ExcelTopHeaderAttribute : ExcelHeaderAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelTopHeaderAttribute"/> class.
        /// </summary>
        public ExcelTopHeaderAttribute(string name) : base(name, 0)
        {
        }
    }
}
