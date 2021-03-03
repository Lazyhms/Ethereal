// Copyright (c) Ethereal. All rights reserved.
//

using Ethereal.Utilities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Update.Internal;
using Microsoft.EntityFrameworkCore.Update;
using System.Linq;

namespace Ethereal.EntityFrameworkCore.SqlServer.Update.Internal
{
    /// <summary>
    /// EtherealSqlServerModificationCommandBatchFactory
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<挂起>")]
    public class EtherealSqlServerModificationCommandBatchFactory : SqlServerModificationCommandBatchFactory
    {
        private readonly ModificationCommandBatchFactoryDependencies _dependencies;
        private readonly IDbContextOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealSqlServerModificationCommandBatchFactory"/> class.
        /// </summary>
        public EtherealSqlServerModificationCommandBatchFactory(
            [NotNull] ModificationCommandBatchFactoryDependencies dependencies,
            [NotNull] IDbContextOptions options) : base(dependencies, options)
        {
            Check.NotNull(dependencies, nameof(dependencies));
            Check.NotNull(options, nameof(options));

            _dependencies = dependencies;
            _options = options;
        }

        /// <inheritdoc/>
        public override ModificationCommandBatch Create()
        {
            var optionsExtension = _options.Extensions.OfType<SqlServerOptionsExtension>().FirstOrDefault();
            return new EtherealSqlServerModificationCommandBatch(_dependencies, optionsExtension?.MaxBatchSize);
        }
    }
}
