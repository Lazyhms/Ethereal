// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore.SqlServer.Update.Internal;
using Microsoft.EntityFrameworkCore.Update;
using System.Collections.Generic;
using System.Text;

namespace Ethereal.EntityFrameworkCore.SqlServer.Update.Internal
{
    /// <summary>
    /// EtherealSqlServerUpdateSqlGenerator
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<挂起>")]
    public class EtherealSqlServerUpdateSqlGenerator : SqlServerUpdateSqlGenerator, IEtherealSqlServerUpdateSqlGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealSqlServerUpdateSqlGenerator"/> class.
        /// </summary>
        public EtherealSqlServerUpdateSqlGenerator(UpdateSqlGeneratorDependencies dependencies) : base(dependencies)
        {
        }

        /// <inheritdoc/>
        public ResultSetMapping AppendBulkDeletedOperation(StringBuilder commandStringBuilder, IReadOnlyList<ModificationCommand> modificationCommands, int commandPosition) => ResultSetMapping.NoResultSet;
    }
}