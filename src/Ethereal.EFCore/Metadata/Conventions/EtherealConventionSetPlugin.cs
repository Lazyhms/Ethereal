// Copyright (c) Ethereal. All rights reserved.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace Ethereal.EntityFrameworkCore.Metadata.Conventions
{
    /// <summary>
    /// EtherealConventionSetPlugin modify conventions
    /// </summary>
    public class EtherealConventionSetPlugin : IConventionSetPlugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealConventionSetPlugin"/> class.
        /// </summary>
        public EtherealConventionSetPlugin([NotNull] ProviderConventionSetBuilderDependencies dependencies) => Dependencies = dependencies;

        /// <summary>
        /// Dependencies
        /// </summary>
        private ProviderConventionSetBuilderDependencies Dependencies { get; }

        /// <inheritdoc/>
        public virtual ConventionSet ModifyConventions(ConventionSet conventionSet)
        {
            // soft delete
            var etherealTableSoftDeleteConvention = new EtherealTableSoftDeleteConvention();
            conventionSet.EntityTypeAddedConventions.Add(etherealTableSoftDeleteConvention);
            // query filters
            var etherealTableQueryFilterConvention = new EtherealTableQueryFilterConvention(Dependencies);
            conventionSet.PropertyAddedConventions.Add(etherealTableQueryFilterConvention);
            // column default value
            var etherealColumnDefaultValueConvention = new EtherealColumnDefaultValueConvention(Dependencies);
            conventionSet.PropertyAddedConventions.Add(etherealColumnDefaultValueConvention);
            conventionSet.PropertyFieldChangedConventions.Add(etherealColumnDefaultValueConvention);
            // column default value sql
            var etherealColumnDefaultValueSqlConvention = new EtherealColumnDefaultValueSqlConvention(Dependencies);
            conventionSet.PropertyAddedConventions.Add(etherealColumnDefaultValueSqlConvention);
            conventionSet.PropertyFieldChangedConventions.Add(etherealColumnDefaultValueSqlConvention);
            // column UpdateIgnore
            var etherealColumnUpdateIgnoreConvention = new EtherealColumnUpdateIgnoreConvention(Dependencies);
            conventionSet.PropertyAddedConventions.Add(etherealColumnUpdateIgnoreConvention);
            conventionSet.PropertyFieldChangedConventions.Add(etherealColumnUpdateIgnoreConvention);
            // sequence
            var etherealSequenceConvention = new EtherealSequenceConvention(Dependencies);
            conventionSet.EntityTypeAddedConventions.Add(etherealSequenceConvention);
            // column sequence
            var etherealColumnSequenceConvention = new EtherealColumnSequenceValueConvention(Dependencies);
            conventionSet.PropertyAddedConventions.Add(etherealColumnSequenceConvention);
            conventionSet.PropertyFieldChangedConventions.Add(etherealColumnSequenceConvention);

            return conventionSet;
        }
    }
}