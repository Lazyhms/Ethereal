// Copyright (c) Ethereal. All rights reserved.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Update.Internal;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ethereal.EntityFrameworkCore.SqlServer.Update.Internal
{
    /// <summary>
    /// EtherealSqlServerModificationCommandBatch
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<挂起>")]
    public class EtherealSqlServerModificationCommandBatch : SqlServerModificationCommandBatch
    {
        private readonly List<ModificationCommand> _bulkDeleteCommands = new List<ModificationCommand>();

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="EtherealSqlServerModificationCommandBatch"/> class.
        /// </summary>
        public EtherealSqlServerModificationCommandBatch([NotNull] ModificationCommandBatchFactoryDependencies dependencies, int? maxBatchSize) : base(dependencies, maxBatchSize)
        {
        }

        /// <inheritdoc/>
        protected override IEtherealSqlServerUpdateSqlGenerator UpdateSqlGenerator => (IEtherealSqlServerUpdateSqlGenerator)base.UpdateSqlGenerator;

        /// <inheritdoc/>
        protected override string GetCommandText()
                   => base.GetCommandText() + GetBulkDeleteCommandText(ModificationCommands.Count);

        /// <inheritdoc/>
        protected override void UpdateCachedCommandText(int commandPosition)
        {
            var newModificationCommand = ModificationCommands[commandPosition];

            if (newModificationCommand.EntityState == EntityState.Deleted)
            {
                if (_bulkDeleteCommands.Count > 0
                    && !CanBeInsertedInSameStatement(_bulkDeleteCommands[0], newModificationCommand))
                {
                    CachedCommandText.Append(GetBulkDeleteCommandText(commandPosition));
                    _bulkDeleteCommands.Clear();
                }

                _bulkDeleteCommands.Add(newModificationCommand);

                LastCachedCommandIndex = commandPosition;
            }
            else
            {
                CachedCommandText.Append(GetBulkDeleteCommandText(commandPosition));
                _bulkDeleteCommands.Clear();

                base.UpdateCachedCommandText(commandPosition);
            }
        }

        private static bool CanBeInsertedInSameStatement(ModificationCommand firstCommand, ModificationCommand secondCommand)
            => string.Equals(firstCommand.TableName, secondCommand.TableName, StringComparison.Ordinal)
                && string.Equals(firstCommand.Schema, secondCommand.Schema, StringComparison.Ordinal)
                && firstCommand.ColumnModifications.Where(o => o.IsWrite).Select(o => o.ColumnName).SequenceEqual(
                    secondCommand.ColumnModifications.Where(o => o.IsWrite).Select(o => o.ColumnName))
                && firstCommand.ColumnModifications.Where(o => o.IsRead).Select(o => o.ColumnName).SequenceEqual(
                    secondCommand.ColumnModifications.Where(o => o.IsRead).Select(o => o.ColumnName));

        private string GetBulkDeleteCommandText(int lastIndex)
        {
            if (_bulkDeleteCommands.Count == 0)
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();
            var resultSetMapping = UpdateSqlGenerator.AppendBulkDeletedOperation(
                stringBuilder, _bulkDeleteCommands, lastIndex - _bulkDeleteCommands.Count);
            for (var i = lastIndex - _bulkDeleteCommands.Count; i < lastIndex; i++)
            {
                CommandResultSet[i] = resultSetMapping;
            }

            if (resultSetMapping != ResultSetMapping.NoResultSet)
            {
                CommandResultSet[lastIndex - 1] = ResultSetMapping.LastInResultSet;
            }

            return stringBuilder.ToString();
        }
    }
}