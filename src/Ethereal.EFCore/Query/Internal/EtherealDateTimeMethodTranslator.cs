// Copyright (c) Ethereal. All rights reserved.

using Ethereal.Utilities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.Query.Internal
{
    /// <summary>
    /// DateTimeBetweenMethodTranslator
    /// </summary>
    public class EtherealDateTimeMethodTranslator : IMethodCallTranslator
    {
        private readonly Dictionary<MethodInfo, string> _methodInfoDatePartMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(DateTimeExtensions).GetRequiredRuntimeMethod(nameof(DateTimeExtensions.Between), new[] { typeof(DateTime), typeof(DateTime) ,typeof(DateTime)}), string.Empty },
            { typeof(DateTimeExtensions).GetRequiredRuntimeMethod(nameof(DateTimeExtensions.Between), new[] { typeof(DateTime?), typeof(DateTime) ,typeof(DateTime)}), string.Empty },
        };

        private readonly ISqlExpressionFactory _sqlExpressionFactory;
        private readonly IRelationalTypeMappingSource _typeMappingSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealDateTimeMethodTranslator"/> class.
        /// </summary>
        public EtherealDateTimeMethodTranslator(
            [NotNull] ISqlExpressionFactory sqlExpressionFactory,
            [NotNull] IRelationalTypeMappingSource typeMappingSource)
        {
            _sqlExpressionFactory = sqlExpressionFactory;
            _typeMappingSource = typeMappingSource;
        }

        /// <inheritdoc/>
        public virtual SqlExpression? Translate(
            SqlExpression? instance,
            MethodInfo method,
            IReadOnlyList<SqlExpression> arguments,
            IDiagnosticsLogger<DbLoggerCategory.Query> logger)
        {
            Check.NotNull(method, nameof(method));
            Check.NotNull(arguments, nameof(arguments));
            Check.NotNull(logger, nameof(logger));

            if (_methodInfoDatePartMapping.TryGetValue(method, out var value))
            {
                var start = _sqlExpressionFactory.GreaterThanOrEqual(arguments[0], arguments[1]);
                var end = _sqlExpressionFactory.LessThanOrEqual(arguments[0], arguments[2]);
                return _sqlExpressionFactory.AndAlso(start, end);
            }

            return null;
        }
    }
}