// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// Extension methods for the <see cref="DatabaseFacade" /> returned from <see cref="DbContext.Database" />
    /// </summary>
    public static class EtherealRelationalDatabaseFacadeExtensions
    {
        /// <summary>
        /// ExecuteReader
        /// </summary>
        public static IEnumerable<IReadOnlyDictionary<string, object>> ExecuteSqlReaderInterpolated(
            this DatabaseFacade databaseFacade,
            FormattableString sql)
            => ExecuteSqlReader(databaseFacade, sql.Format, sql.GetArguments()!, resultSelector =>
            {
                var result = new Dictionary<string, object>();

                for (var i = 0; i < resultSelector.FieldCount; i++)
                {
                    result.TryAdd(resultSelector.GetName(i), resultSelector.GetValue(i));
                }

                return result;
            });

        /// <summary>
        /// ExecuteReader
        /// </summary>
        public static async Task<IEnumerable<IReadOnlyDictionary<string, object>>> ExecuteSqlReaderInterpolatedAsync(
            this DatabaseFacade databaseFacade,
            FormattableString sql,
            CancellationToken cancellationToken = default)
            => await ExecuteSqlReaderAsync(databaseFacade, sql.Format, sql.GetArguments()!, resultSelector =>
             {
                 var result = new Dictionary<string, object>();

                 for (var i = 0; i < resultSelector.FieldCount; i++)
                 {
                     result.TryAdd(resultSelector.GetName(i), resultSelector.GetValue(i));
                 }

                 return result;
             }, cancellationToken);

        /// <summary>
        /// ExecuteReader
        /// </summary>
        public static IEnumerable<T> ExecuteSqlReaderInterpolated<T>(
            this DatabaseFacade databaseFacade,
            FormattableString sql) where T : class
            => ExecuteSqlReader(databaseFacade, sql.Format, sql.GetArguments()!, resultSelector =>
            {
                var result = Activator.CreateInstance<T>();

                for (var i = 0; i < resultSelector.FieldCount; i++)
                {
                    var fieldName = resultSelector.GetName(i);
                    if (!string.IsNullOrWhiteSpace(fieldName))
                    {
                        typeof(T).GetProperty(fieldName)?.SetValue(result, resultSelector[fieldName]);
                    }
                }

                return result;
            });

        /// <summary>
        /// ExecuteReader
        /// </summary>
        public static async Task<IEnumerable<T>> ExecuteSqlReaderInterpolatedAsync<T>(
            this DatabaseFacade databaseFacade,
            FormattableString sql,
            CancellationToken cancellationToken = default) where T : class
            => await ExecuteSqlReaderAsync(databaseFacade, sql.Format, sql.GetArguments()!, resultSelector =>
             {
                 var result = Activator.CreateInstance<T>();

                 for (var i = 0; i < resultSelector.FieldCount; i++)
                 {
                     var fieldName = resultSelector.GetName(i);
                     if (!string.IsNullOrWhiteSpace(fieldName))
                     {
                         typeof(T).GetProperty(fieldName)?.SetValue(result, resultSelector[fieldName]);
                     }
                 }

                 return result;
             }, cancellationToken);

        /// <summary>
        /// ExecuteReader
        /// </summary>
        public static IEnumerable<T> ExecuteSqlReaderInterpolated<T>(
            this DatabaseFacade databaseFacade,
            FormattableString sql,
            Func<IDataRecord, T> resultSelector)
            => ExecuteSqlReader(databaseFacade, sql.Format, sql.GetArguments()!, resultSelector);

        /// <summary>
        /// ExecuteReader
        /// </summary>
        public static async Task<IEnumerable<T>> ExecuteSqlReaderInterpolatedAsync<T>(
            this DatabaseFacade databaseFacade,
            FormattableString sql,
            Func<IDataRecord, T> resultSelector,
            CancellationToken cancellationToken = default)
            => await ExecuteSqlReaderAsync(databaseFacade, sql.Format, sql.GetArguments()!, resultSelector, cancellationToken);

        /// <summary>
        /// ExecuteReader
        /// </summary>
        public static IEnumerable<IReadOnlyDictionary<string, object>> ExecuteSqlReader(
            this DatabaseFacade databaseFacade,
            string sql,
            IEnumerable<object>? parameters = default)
            => ExecuteSqlReader(databaseFacade, sql, parameters, resultSelector =>
            {
                var result = new Dictionary<string, object>();

                for (var i = 0; i < resultSelector.FieldCount; i++)
                {
                    result.TryAdd(resultSelector.GetName(i), resultSelector.GetValue(i));
                }

                return result;
            });

        /// <summary>
        /// ExecuteReader
        /// </summary>
        public static IEnumerable<T> ExecuteSqlReader<T>(
            this DatabaseFacade databaseFacade,
            string sql,
            IEnumerable<object>? parameters = default) where T : class
            => ExecuteSqlReader(databaseFacade, sql, parameters, resultSelector =>
            {
                var result = Activator.CreateInstance<T>();

                for (var i = 0; i < resultSelector.FieldCount; i++)
                {
                    var fieldName = resultSelector.GetName(i);
                    if (!string.IsNullOrWhiteSpace(fieldName))
                    {
                        typeof(T).GetProperty(fieldName)?.SetValue(result, resultSelector[fieldName]);
                    }
                }

                return result;
            });

        /// <summary>
        /// ExecuteReader
        /// </summary>
        public static async Task<IEnumerable<IReadOnlyDictionary<string, object>>> ExecuteSqlReaderAsync(
            this DatabaseFacade databaseFacade,
            string sql,
            IEnumerable<object>? parameters = default,
            CancellationToken cancellationToken = default)
            => await ExecuteSqlReaderAsync(databaseFacade, sql, parameters, resultSelector =>
             {
                 var result = new Dictionary<string, object>();

                 for (var i = 0; i < resultSelector.FieldCount; i++)
                 {
                     result.TryAdd(resultSelector.GetName(i), resultSelector.GetValue(i));
                 }

                 return result;
             }, cancellationToken);

        /// <summary>
        /// ExecuteReader
        /// </summary>
        public static async Task<IEnumerable<T>> ExecuteSqlReaderAsync<T>(
            this DatabaseFacade databaseFacade,
            string sql,
            IEnumerable<object>? parameters = default,
            CancellationToken cancellationToken = default) where T : class
            => await ExecuteSqlReaderAsync(databaseFacade, sql, parameters, resultSelector =>
            {
                var result = Activator.CreateInstance<T>();

                for (var i = 0; i < resultSelector.FieldCount; i++)
                {
                    var fieldName = resultSelector.GetName(i);
                    if (!string.IsNullOrWhiteSpace(fieldName))
                    {
                        typeof(T).GetProperty(fieldName)?.SetValue(result, resultSelector[fieldName]);
                    }
                }

                return result;
            }, cancellationToken);

        /// <summary>
        /// ExecuteReader
        /// </summary>
        public static IEnumerable<T> ExecuteSqlReader<T>(
            this DatabaseFacade databaseFacade,
            string sql,
            IEnumerable<object>? parameters,
            Func<IDataRecord, T> resultSelector)
        {
            var facadeDependencies = GetFacadeDependencies(databaseFacade);
            var concurrencyDetector = facadeDependencies.CoreOptions.AreThreadSafetyChecksEnabled
                ? facadeDependencies.ConcurrencyDetector
                : null;
            var logger = facadeDependencies.CommandLogger;

            concurrencyDetector?.EnterCriticalSection();

            try
            {
                var rawSqlCommand = facadeDependencies.RawSqlCommandBuilder
                    .Build(sql, parameters ?? Enumerable.Empty<object>());

                var relationalDataReader = rawSqlCommand
                    .RelationalCommand
                    .ExecuteReader(
                        new RelationalCommandParameterObject(
                            facadeDependencies.RelationalConnection,
                            rawSqlCommand.ParameterValues,
                            null,
                            ((IDatabaseFacadeDependenciesAccessor)databaseFacade).Context,
                            logger, CommandSource.ExecuteSqlRaw));

                return relationalDataReader.DbDataReader.Cast<IDataRecord>().Select(resultSelector);
            }
            finally
            {
                concurrencyDetector?.ExitCriticalSection();
            }
        }

        /// <summary>
        /// ExecuteReader
        /// </summary>
        public static async Task<IEnumerable<T>> ExecuteSqlReaderAsync<T>(
            this DatabaseFacade databaseFacade,
            string sql,
            IEnumerable<object>? parameters,
            Func<IDataRecord, T> resultSelector,
            CancellationToken cancellationToken = default)
        {
            var facadeDependencies = GetFacadeDependencies(databaseFacade);
            var concurrencyDetector = facadeDependencies.CoreOptions.AreThreadSafetyChecksEnabled
                ? facadeDependencies.ConcurrencyDetector
                : null;
            var logger = facadeDependencies.CommandLogger;

            concurrencyDetector?.EnterCriticalSection();

            try
            {
                var rawSqlCommand = facadeDependencies.RawSqlCommandBuilder
                    .Build(sql, parameters ?? Enumerable.Empty<object>());

                var relationalDataReader = await rawSqlCommand
                   .RelationalCommand
                   .ExecuteReaderAsync(
                       new RelationalCommandParameterObject(
                           facadeDependencies.RelationalConnection,
                           rawSqlCommand.ParameterValues,
                           null,
                           ((IDatabaseFacadeDependenciesAccessor)databaseFacade).Context,
                           logger, CommandSource.ExecuteSqlRaw), cancellationToken);

                return relationalDataReader.DbDataReader.Cast<IDataRecord>().Select(resultSelector);
            }
            finally
            {
                concurrencyDetector?.ExitCriticalSection();
            }
        }

        private static IRelationalDatabaseFacadeDependencies GetFacadeDependencies(DatabaseFacade databaseFacade)
        {
            var dependencies = ((IDatabaseFacadeDependenciesAccessor)databaseFacade).Dependencies;

            if (dependencies is IRelationalDatabaseFacadeDependencies relationalDependencies)
            {
                return relationalDependencies;
            }

            throw new InvalidOperationException(RelationalStrings.RelationalNotInUse);
        }
    }
}
