using Microsoft.Extensions.Logging;
using System;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Logging
{
    /// <summary>
    /// <see cref="ILogger"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    internal static partial class LoggerExtensionsIncludeInvalidPagingPredicate
    {
        private static readonly Action<ILogger, string, string, Exception> IncludeInvalidPagingPredicateAction = LoggerMessage.Define<string, string>(
            LogLevel.Warning,
            new EventId(id: default, name: nameof(IncludeInvalidPagingPredicate)),
            "Include invalid paging predicate. Offset: {Offset}. Limit: {Limit}.");

        public static void IncludeInvalidPagingPredicate(this ILogger logger, string offset, string limit)
            => IncludeInvalidPagingPredicateAction(logger, offset, limit, null);
    }
}
