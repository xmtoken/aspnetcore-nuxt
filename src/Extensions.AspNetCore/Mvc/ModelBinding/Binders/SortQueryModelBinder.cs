using AspNetCoreNuxt.Extensions.AspNetCore.Logging;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels;
using AspNetCoreNuxt.Extensions.Collections;
using AspNetCoreNuxt.Extensions.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ModelBinding.Binders
{
    /// <summary>
    /// <see cref="SortQuery{T}"/> クラスのモデルバインダーを表します。
    /// </summary>
    /// <typeparam name="T">コンポーネントの型。</typeparam>
    public class SortQueryModelBinder<T> : IModelBinder
    {
        /// <summary>
        /// <see cref="ILogger"/> オブジェクトを表します。
        /// </summary>
        private readonly ILogger Logger;

        /// <summary>
        /// <see cref="SortQueryModelBinder{T}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> オブジェクト。</param>
        public SortQueryModelBinder(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<SortQueryModelBinder<T>>();
        }

        /// <inheritdoc/>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(SortQuery<T>.BindingModelName);
            if (valueProviderResult.Length == 0)
            {
                bindingContext.Result = ModelBindingResult.Success(SortQuery<T>.Default);
                return Task.CompletedTask;
            }

            var splitValues = valueProviderResult
                .Values
                .ToString()
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Where(x => string.IsNullOrWhiteSpace(x) == false)
                .Select(x => x.Trim().ToUpperInvariant())
                .Distinct()
                .ToArray();

            var validValues = splitValues
                .Where(x => x.StartsWith("+", StringComparison.Ordinal)
                         || x.StartsWith("-", StringComparison.Ordinal))
                .Where(x => ExpressionHelper.CanConvertToExpression<T>(x[1..]))
                .Select(x => new Sorting(
                    ExpressionHelper.Normalize<T>(x[1..]),
                    x[0] == '+' ? SortDirection.Ascending : SortDirection.Descending))
                .ToArray();

            if (splitValues.Length != validValues.Length)
            {
                Logger.IncludeInvalidSortPredicate(valueProviderResult.Values);
            }

            var model = new SortQuery<T>(validValues, valueProviderResult.Values);
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}
