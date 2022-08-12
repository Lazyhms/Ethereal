// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Reflection;

namespace Ethereal.EFCore.PostgreSQL.Query.Internal;

/// <summary>
/// NpgsqlIEnumerableDbFunctionsTranslator
/// </summary>
public class EtherealNpgsqlIEnumerableDbFunctionsTranslator : IMethodCallTranslator
{
    private readonly ISqlExpressionFactory _sqlExpressionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="EtherealNpgsqlIEnumerableDbFunctionsTranslator"/> class.
    /// </summary>
    public EtherealNpgsqlIEnumerableDbFunctionsTranslator(ISqlExpressionFactory sqlExpressionFactory)
    {
        _sqlExpressionFactory = sqlExpressionFactory;
    }

    /// <inheritdoc/>
    public SqlExpression? Translate(SqlExpression? instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments, IDiagnosticsLogger<DbLoggerCategory.Query> logger)
    {
        if (typeof(EtherealNpgsqlIEnumerableDbFunctionsExtensions).IsAssignableFrom(method.DeclaringType))
        {
            return _sqlExpressionFactory.Function(method.Name switch
            {
                nameof(EtherealNpgsqlIEnumerableDbFunctionsExtensions.Unnest) => "unnest",
                nameof(EtherealNpgsqlIEnumerableDbFunctionsExtensions.Any) => "Any",
                _ => throw new InvalidOperationException($"Could not find method '{method.Name}' on type '{typeof(EtherealNpgsqlIEnumerableDbFunctionsExtensions)}'"),
            }, arguments.Skip(1), true, argumentsPropagateNullability: new[] { false, true }, method.ReturnType);
        }
        return null;
    }
}
