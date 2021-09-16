// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.Metadata.Conventions
{
    /// <summary>
    /// EtherealColumnUpdateIgnoreConvention
    /// </summary>
    public class EtherealColumnInsertIgnoreConvention : PropertyAttributeConventionBase<InsertIgnoreAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealColumnInsertIgnoreConvention"/> class.
        /// </summary>
        public EtherealColumnInsertIgnoreConvention(ProviderConventionSetBuilderDependencies dependencies) : base(dependencies)
        {
        }

        /// <inheritdoc/>
        protected override void ProcessPropertyAdded(
            IConventionPropertyBuilder propertyBuilder,
            InsertIgnoreAttribute attribute,
            MemberInfo clrMember,
            IConventionContext context)
        {
            if (propertyBuilder.CanSetBeforeSave(PropertySaveBehavior.Ignore, true))
            {
                propertyBuilder.BeforeSave(PropertySaveBehavior.Ignore, true);
            }
        }
    }
}