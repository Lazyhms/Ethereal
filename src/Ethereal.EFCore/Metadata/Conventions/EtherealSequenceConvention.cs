// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace Ethereal.EntityFrameworkCore.Metadata.Conventions
{
    /// <summary>
    /// EtherealColumnSequenceConvention
    /// </summary>
    public class EtherealSequenceConvention : EntityTypeAttributeConventionBase<SequenceAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealSequenceConvention"/> class.
        /// </summary>
        public EtherealSequenceConvention(ProviderConventionSetBuilderDependencies dependencies) : base(dependencies)
        {
        }

        /// <inheritdoc/>
        protected override void ProcessEntityTypeAdded(
            IConventionEntityTypeBuilder entityTypeBuilder,
            SequenceAttribute attribute,
            IConventionContext<IConventionEntityTypeBuilder> context)
        {
            var sequenceBuilder = entityTypeBuilder.ModelBuilder.HasSequence(attribute.Name, attribute.Schema, true);

            if (sequenceBuilder.CanSetType(attribute.Type, true))
            {
                sequenceBuilder.HasType(attribute.Type, true);
            }
            if (sequenceBuilder.CanSetStartsAt(attribute.StartValue, true))
            {
                sequenceBuilder.StartsAt(attribute.StartValue, true);
            }
            if (sequenceBuilder.CanSetIncrementsBy(attribute.IncrementBy, true))
            {
                sequenceBuilder.IncrementsBy(attribute.IncrementBy, true);
            }
            if (sequenceBuilder.CanSetIsCyclic(attribute.IsCyclic, true))
            {
                sequenceBuilder.IsCyclic(attribute.IsCyclic, true);
            }
            if (sequenceBuilder.CanSetMin(attribute.Minimum, true))
            {
                sequenceBuilder.HasMin(attribute.Minimum, true);
            }
            if (sequenceBuilder.CanSetMax(attribute.Maximum, true))
            {
                sequenceBuilder.HasMax(attribute.Maximum, true);
            }
        }
    }
}