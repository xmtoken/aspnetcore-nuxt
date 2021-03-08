using Microsoft.Extensions.Logging;
using System;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Logging
{
    /// <summary>
    /// <see cref="ILogger"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    internal static partial class LoggerExtensionsIncludeInvalidFieldPredicate
    {
        private static readonly Action<ILogger, string, Exception> IncludeInvalidFieldPredicateAction = LoggerMessage.Define<string>(
            LogLevel.Warning,
            new EventId(id: default, name: nameof(IncludeInvalidFieldPredicate)),
            "Include invalid field predicate. Query: {Query}.");

        public static void IncludeInvalidFieldPredicate(this ILogger logger, string query)
            => IncludeInvalidFieldPredicateAction(logger, query, null);
    }
}
