// Copyright (c) Ethereal. All rights reserved.
//

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.SqlServer.Update.Internal;
using Microsoft.EntityFrameworkCore.Update;
using System.Collections.Generic;
using System.Text;

namespace Ethereal.EntityFrameworkCore.SqlServer.Update.Internal
{
    /// <summary>
    /// IEtherealSqlServerUpdateSqlGenerator
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<挂起>")]
    public interface IEtherealSqlServerUpdateSqlGenerator : ISqlServerUpdateSqlGenerator
    {
        /// <summary>
        /// AppendBulkDeletedOperation
        /// </summary>
        ResultSetMapping AppendBulkDeletedOperation(
            [NotNull] StringBuilder commandStringBuilder,
            [NotNull] IReadOnlyList<ModificationCommand> modificationCommands,
            int commandPosition);
    }
}
