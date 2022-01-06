// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
        /// Dependencies
        /// </summary>
        protected ProviderConventionSetBuilderDependencies Dependencies { get; }

        /// <summary>
        /// EtherealOptions
        /// </summary>
        protected IEtherealOptions EtherealOptions { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealConventionSetPlugin"/> class.
        /// </summary>
        public EtherealConventionSetPlugin(
            IEtherealOptions etherealOptions,
            ProviderConventionSetBuilderDependencies dependencies)
        {
            Dependencies = dependencies;
            EtherealOptions = etherealOptions;
        }

        /// <inheritdoc/>
        public virtual ConventionSet ModifyConventions(ConventionSet conventionSet)
        {
            conventionSet.EntityTypeAddedConventions.Add(new EtherealTableSoftDeleteConvention());

            var etherealColumnDefaultValueConvention = new EtherealColumnDefaultValueConvention(Dependencies);
            conventionSet.PropertyAddedConventions.Add(etherealColumnDefaultValueConvention);
            conventionSet.PropertyFieldChangedConventions.Add(etherealColumnDefaultValueConvention);

            var etherealColumnDefaultValueSqlConvention = new EtherealColumnDefaultValueSqlConvention(Dependencies);
            conventionSet.PropertyAddedConventions.Add(etherealColumnDefaultValueSqlConvention);
            conventionSet.PropertyFieldChangedConventions.Add(etherealColumnDefaultValueSqlConvention);

            var etherealColumnUpdateIgnoreConvention = new EtherealColumnUpdateIgnoreConvention(Dependencies);
            conventionSet.PropertyAddedConventions.Add(etherealColumnUpdateIgnoreConvention);
            conventionSet.PropertyFieldChangedConventions.Add(etherealColumnUpdateIgnoreConvention);

            var etherealColumnInsertIgnoreConvention = new EtherealColumnAddIgnoreConvention(Dependencies);
            conventionSet.PropertyAddedConventions.Add(etherealColumnInsertIgnoreConvention);
            conventionSet.PropertyFieldChangedConventions.Add(etherealColumnInsertIgnoreConvention);

            if (EtherealOptions.NamingPolicy != NamingPolicy.None)
            {
                var etherealNamingPolicyConvention = new EtherealNamingPolicyConvention(EtherealOptions.NamingPolicy);
                conventionSet.EntityTypeAddedConventions.Add(etherealNamingPolicyConvention);
                conventionSet.EntityTypeBaseTypeChangedConventions.Add(etherealNamingPolicyConvention);
                conventionSet.KeyAddedConventions.Add(etherealNamingPolicyConvention);
                conventionSet.IndexAddedConventions.Add(etherealNamingPolicyConvention);
                conventionSet.PropertyAddedConventions.Add(etherealNamingPolicyConvention);
                conventionSet.ForeignKeyAddedConventions.Add(etherealNamingPolicyConvention);
                conventionSet.PropertyFieldChangedConventions.Add(etherealNamingPolicyConvention);
                conventionSet.ForeignKeyOwnershipChangedConventions.Add(etherealNamingPolicyConvention);
            }
            return conventionSet;
        }
    }
}