// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// RemoveForeignKey
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<挂起>")]
    public class EtherealMigrationsModelDiffer : MigrationsModelDiffer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealMigrationsModelDiffer"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<挂起>")]
        public EtherealMigrationsModelDiffer(
            IRelationalTypeMappingSource typeMappingSource,
            IMigrationsAnnotationProvider migrationsAnnotations,
            IChangeDetector changeDetector,
            IUpdateAdapterFactory updateAdapterFactory,
            CommandBatchPreparerDependencies commandBatchPreparerDependencies) :
            base(typeMappingSource, migrationsAnnotations, changeDetector, updateAdapterFactory, commandBatchPreparerDependencies)
        {
        }

        /// <inheritdoc/>
        protected override IEnumerable<MigrationOperation> Add(IForeignKeyConstraint target, DiffContext diffContext)
        {
            return Enumerable.Empty<MigrationOperation>();
        }

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<挂起>")]
        protected override IEnumerable<MigrationOperation> Diff(IForeignKeyConstraint source, IForeignKeyConstraint target, DiffContext diffContext)
        {
            return Enumerable.Empty<MigrationOperation>();
        }
        /// <inheritdoc/>
        protected override IEnumerable<MigrationOperation> Remove(IForeignKeyConstraint source, DiffContext diffContext)
        {
            return Enumerable.Empty<MigrationOperation>();
        }

        /// <inheritdoc/>
        protected override IEnumerable<MigrationOperation> Add(ITable target, DiffContext diffContext)
        {
            return base.Add(target, diffContext);
        }
    }
}