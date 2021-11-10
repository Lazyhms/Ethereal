// Copyright (c) Ethereal. All rights reserved.

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
        public EtherealConventionSetPlugin(ProviderConventionSetBuilderDependencies dependencies)
        {
            Dependencies = dependencies;
        }

        /// <summary>
        /// Dependencies
        /// </summary>
        private ProviderConventionSetBuilderDependencies Dependencies { get; }

        /// <inheritdoc/>
        public virtual ConventionSet ModifyConventions(ConventionSet conventionSet)
        {
            conventionSet.EntityTypeAddedConventions.Add(new EtherealTableSoftDeleteConvention());

            conventionSet.EntityTypeAddedConventions.Add(new EtherealSequenceConvention(Dependencies));

            var etherealColumnSequenceConvention = new EtherealColumnSequenceValueConvention(Dependencies);
            conventionSet.PropertyAddedConventions.Add(etherealColumnSequenceConvention);
            conventionSet.PropertyFieldChangedConventions.Add(etherealColumnSequenceConvention);

            var etherealTableQueryFilterConvention = new EtherealTableQueryFilterConvention(Dependencies);
            conventionSet.PropertyAddedConventions.Add(etherealTableQueryFilterConvention);
            conventionSet.PropertyFieldChangedConventions.Add(etherealTableQueryFilterConvention);

            var etherealColumnDefaultValueConvention = new EtherealColumnDefaultValueConvention(Dependencies);
            conventionSet.PropertyAddedConventions.Add(etherealColumnDefaultValueConvention);
            conventionSet.PropertyFieldChangedConventions.Add(etherealColumnDefaultValueConvention);

            var etherealColumnDefaultValueSqlConvention = new EtherealColumnDefaultValueSqlConvention(Dependencies);
            conventionSet.PropertyAddedConventions.Add(etherealColumnDefaultValueSqlConvention);
            conventionSet.PropertyFieldChangedConventions.Add(etherealColumnDefaultValueSqlConvention);

            var etherealColumnUpdateIgnoreConvention = new EtherealColumnUpdateIgnoreConvention(Dependencies);
            conventionSet.PropertyAddedConventions.Add(etherealColumnUpdateIgnoreConvention);
            conventionSet.PropertyFieldChangedConventions.Add(etherealColumnUpdateIgnoreConvention);

            var etherealColumnInsertIgnoreConvention = new EtherealColumnInsertIgnoreConvention(Dependencies);
            conventionSet.PropertyAddedConventions.Add(etherealColumnInsertIgnoreConvention);
            conventionSet.PropertyFieldChangedConventions.Add(etherealColumnInsertIgnoreConvention);

            return conventionSet;
        }
    }
}