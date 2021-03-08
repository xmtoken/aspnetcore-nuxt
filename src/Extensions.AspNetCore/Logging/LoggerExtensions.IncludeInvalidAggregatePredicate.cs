using Microsoft.Extensions.Logging;
using System;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Logging
{
    /// <summary>
    /// <see cref="ILogger"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    internal static partial class LoggerExtensionsIncludeInvalidAggregatePredicate
    {
        private static readonly Action<ILogger, string, Exception> IncludeInvalidAggregatePredicateAction = LoggerMessage.Define<string>(
            LogLevel.Warning,
            new EventId(id: default, name: nameof(IncludeInvalidAggregatePredicate)),
            "Include invalid aggregate predicate. Query: {Query}.");

        public static void IncludeInvalidAggregatePredicate(this ILogger logger, string query)
            => IncludeInvalidAggregatePredicateAction(logger, query, null);
    }
}
