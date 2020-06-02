using Polly;
using Polly.Retry;
using System;

namespace AspNetCoreNuxt.Extensions.Polly
{
    /// <summary>
    /// <see cref="PolicyBuilder"/> クラスの拡張機能を提供します。
    /// </summary>
    public static class PolicyBuilderExtensions
    {
        /// <inheritdoc cref="RetrySyntax.RetryForever(PolicyBuilder, Action{Exception})"/>
        /// <typeparam name="TException">ハンドルする例外の型。</typeparam>
        public static RetryPolicy RetryForever<TException>(this PolicyBuilder policyBuilder, Action<TException> onRetry)
            where TException : Exception
            => policyBuilder.RetryForever(ex => onRetry((TException)ex));

        /// <inheritdoc cref="AsyncRetrySyntax.RetryForeverAsync(PolicyBuilder, Action{Exception})"/>
        /// <typeparam name="TException">ハンドルする例外の型。</typeparam>
        public static AsyncRetryPolicy RetryForeverAsync<TException>(this PolicyBuilder policyBuilder, Action<TException> onRetry)
            where TException : Exception
            => policyBuilder.RetryForeverAsync(ex => onRetry((TException)ex));
    }
}
