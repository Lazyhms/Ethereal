// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System.Reflection;

namespace Ethereal.EntityFrameworkCore.Metadata.Conventions
{
    /// <summary>
    /// EtherealColumnDefaultValueAttributeConvention
    /// </summary>
    public class EtherealColumnDefaultValueSqlConvention : PropertyAttributeConventionBase<DefaultValueSqlAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealColumnDefaultValueSqlConvention"/> class.
        /// </summary>
        public EtherealColumnDefaultValueSqlConvention(ProviderConventionSetBuilderDependencies dependencies) : base(dependencies)
        {
        }

        /// <inheritdoc/>
        protected override void ProcessPropertyAdded(
            IConventionPropertyBuilder propertyBuilder,
            DefaultValueSqlAttribute attribute,
            MemberInfo clrMember,
            IConventionContext context)
        {
            if (!string.IsNullOrWhiteSpace(attribute.Value))
            {
                propertyBuilder.HasDefaultValueSql(attribute.Value, true);
            }
        }
    }
}