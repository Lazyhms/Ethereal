// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System.Reflection;

namespace Ethereal.EntityFrameworkCore.Metadata.Conventions
{
    /// <summary>
    /// EtherealColumnSequenceConvention
    /// </summary>
    public class EtherealColumnSequenceValueConvention : PropertyAttributeConventionBase<SequenceValueAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealColumnSequenceValueConvention"/> class.
        /// </summary>
        public EtherealColumnSequenceValueConvention(ProviderConventionSetBuilderDependencies dependencies) : base(dependencies)
        {
        }

        /// <inheritdoc/>
        protected override void ProcessPropertyAdded(
            IConventionPropertyBuilder propertyBuilder,
            SequenceValueAttribute attribute,
            MemberInfo clrMember,
            IConventionContext context)
        {
            if (!string.IsNullOrWhiteSpace(attribute.Name))
            {
                propertyBuilder.HasDefaultValueSql($"NEXT VALUE FOR {(string.IsNullOrWhiteSpace(attribute.Schema) ? string.Empty : $"[{attribute.Schema}].")}[{attribute.Name}]", true);
            }
        }
    }
}