using Microsoft.Extensions.Logging;
using System;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Logging
{
    /// <summary>
    /// <see cref="ILogger"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    internal static partial class LoggerExtensionsIncludeInvalidGroupPredicate
    {
        private static readonly Action<ILogger, string, Exception> IncludeInvalidGroupPredicateAction = LoggerMessage.Define<string>(
            LogLevel.Warning,
            new EventId(id: default, name: nameof(IncludeInvalidGroupPredicate)),
            "Include invalid group predicate. Query: {Query}.");

        public static void IncludeInvalidGroupPredicate(this ILogger logger, string query)
            => IncludeInvalidGroupPredicateAction(logger, query, null);
    }
}
