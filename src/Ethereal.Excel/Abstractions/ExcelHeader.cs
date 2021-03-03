// Copyright (c) Ethereal. All rights reserved.
//

using System;

namespace Ethereal.Excel
{
    /// <summary>
    /// ExcelHeader
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ExcelHeaderAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelHeaderAttribute"/> class.
        /// </summary>
        public ExcelHeaderAttribute(string name, int ordinal)
        {
            Name = name;
            Ordinal = ordinal;
        }

        /// <summary>
        /// Order
        /// </summary>
        public int Ordinal { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
    }
}
