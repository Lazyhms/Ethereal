// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System.Reflection;

namespace Ethereal.EntityFrameworkCore.Metadata.Conventions
{
    /// <summary>
    /// EtherealColumnUpdateIgnoreConvention
    /// </summary>
    public sealed class EtherealColumnUpdateIgnoreConvention : PropertyAttributeConventionBase<UpdateIgnoreAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealColumnUpdateIgnoreConvention"/> class.
        /// </summary>
        public EtherealColumnUpdateIgnoreConvention(ProviderConventionSetBuilderDependencies dependencies) : base(dependencies)
        {
        }

        /// <inheritdoc/>
        protected override void ProcessPropertyAdded(
            IConventionPropertyBuilder propertyBuilder,
            UpdateIgnoreAttribute attribute,
            MemberInfo clrMember,
            IConventionContext context)
        {
            if (propertyBuilder.CanSetAfterSave(PropertySaveBehavior.Ignore, true))
            {
                propertyBuilder.AfterSave(PropertySaveBehavior.Ignore, true);
            }
        }
    }
}