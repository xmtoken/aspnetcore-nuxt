using Microsoft.Extensions.Logging;
using System;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Logging
{
    /// <summary>
    /// <see cref="ILogger"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    internal static class LoggerExtensionsIncludeInvalidSearchConditionsPredicate
    {
        private static readonly Action<ILogger, string, Exception> IncludeInvalidSearchConditionsPredicateAction = LoggerMessage.Define<string>(
            LogLevel.Warning,
            new EventId(id: default, name: nameof(IncludeInvalidSearchConditionsPredicate)),
            "Include invalid search conditions predicate. Query: {Query}.");

        public static void IncludeInvalidSearchConditionsPredicate(this ILogger logger, string query)
            => IncludeInvalidSearchConditionsPredicateAction(logger, query, null);
    }
}
