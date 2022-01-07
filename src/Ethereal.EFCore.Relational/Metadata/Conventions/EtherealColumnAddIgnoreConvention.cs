// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.Metadata.Conventions
{
    /// <summary>
    /// EtherealColumnUpdateIgnoreConvention
    /// </summary>
    public sealed class EtherealColumnAddIgnoreConvention : PropertyAttributeConventionBase<AddIgnoreAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealColumnAddIgnoreConvention"/> class.
        /// </summary>
        public EtherealColumnAddIgnoreConvention(ProviderConventionSetBuilderDependencies dependencies) : base(dependencies)
        {
        }

        /// <inheritdoc/>
        protected override void ProcessPropertyAdded(
            IConventionPropertyBuilder propertyBuilder,
            AddIgnoreAttribute attribute,
            MemberInfo clrMember,
            IConventionContext context)
        {
            if (propertyBuilder.CanSetBeforeSave(PropertySaveBehavior.Ignore))
            {
                propertyBuilder.BeforeSave(PropertySaveBehavior.Ignore);
            }
        }
    }
}