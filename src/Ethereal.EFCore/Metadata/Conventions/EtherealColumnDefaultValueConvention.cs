// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System.ComponentModel;
using System.Reflection;

namespace Ethereal.EntityFrameworkCore.Metadata.Conventions
{
    /// <summary>
    /// EtherealColumnDefaultValueAttributeConvention
    /// </summary>
    public class EtherealColumnDefaultValueConvention : PropertyAttributeConventionBase<DefaultValueAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealColumnDefaultValueConvention"/> class.
        /// </summary>
        public EtherealColumnDefaultValueConvention(ProviderConventionSetBuilderDependencies dependencies) : base(dependencies)
        {
        }

        /// <inheritdoc/>
        protected override void ProcessPropertyAdded(
            IConventionPropertyBuilder propertyBuilder,
            DefaultValueAttribute attribute,
            MemberInfo clrMember,
            IConventionContext context)
        {
            if (propertyBuilder.CanSetDefaultValue(attribute.Value, true))
            {
                propertyBuilder.HasDefaultValue(attribute.Value, true);
            }
        }
    }
}