// Copyright (c) Ethereal. All rights reserved.
//

using JetBrains.Annotations;
using System.Collections.Generic;

namespace Ethereal.Excel.Metadata
{
    /// <summary>
    /// IEntityType
    /// </summary>
    public interface IEntityType
    {
        /// <summary>
        /// FindProperty
        /// </summary>
        IProperty? FindProperty([NotNull] string name);

        /// <summary>
        /// GetProperties
        /// </summary>
        IEnumerable<IProperty> GetProperties();
    }
}
