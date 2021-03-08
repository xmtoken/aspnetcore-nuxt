using Microsoft.Extensions.Logging;
using System;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Logging
{
    /// <summary>
    /// <see cref="ILogger"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    internal static class LoggerExtensionsIncludeInvalidSortPredicate
    {
        private static readonly Action<ILogger, string, Exception> IncludeInvalidSortPredicateAction = LoggerMessage.Define<string>(
            LogLevel.Warning,
            new EventId(id: default, name: nameof(IncludeInvalidSortPredicate)),
            "Include invalid sort predicate. Query: {Query}.");

        public static void IncludeInvalidSortPredicate(this ILogger logger, string query)
            => IncludeInvalidSortPredicateAction(logger, query, null);
    }
}
