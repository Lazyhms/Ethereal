// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;

namespace Microsoft.EntityFrameworkCore.Query.Internal
{
    /// <summary>
    /// DateTimeMethodCallTranslatorPlugin
    /// </summary>
    public class EtherealDateTimeMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="EtherealDateTimeMethodCallTranslatorPlugin"/> class.
        /// </summary>
        public EtherealDateTimeMethodCallTranslatorPlugin(
            ISqlExpressionFactory sqlExpressionFactory,
            IRelationalTypeMappingSource typeMappingSource) =>
            Translators = new IMethodCallTranslator[] {
                new EtherealDateTimeMethodTranslator(sqlExpressionFactory,typeMappingSource)
            };

        /// <inheritdoc/>
        public IEnumerable<IMethodCallTranslator> Translators { get; }
    }
}