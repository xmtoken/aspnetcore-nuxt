using AspNetCoreNuxt.Extensions.AspNetCore.Logging;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels;
using AspNetCoreNuxt.Extensions.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ModelBinding.Binders
{
    /// <summary>
    /// <see cref="FieldQuery{T}"/> クラスのモデルバインダーを表します。
    /// </summary>
    /// <typeparam name="T">コンポーネントの型。</typeparam>
    public class FieldQueryModelBinder<T> : IModelBinder
    {
        /// <summary>
        /// <see cref="ILogger"/> オブジェクトを表します。
        /// </summary>
        private readonly ILogger Logger;

        /// <summary>
        /// <see cref="FieldQueryModelBinder{T}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> オブジェクト。</param>
        public FieldQueryModelBinder(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<FieldQueryModelBinder<T>>();
        }

        /// <inheritdoc/>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(FieldQuery<T>.BindingModelName);
            if (valueProviderResult.Length == 0)
            {
                bindingContext.Result = ModelBindingResult.Success(FieldQuery<T>.Default);
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
                .Where(x => ExpressionHelper.Validate<T>(x))
                .Select(x => ExpressionHelper.Normalize<T>(x))
                .ToArray();

            if (splitValues.Length != validValues.Length)
            {
                Logger.IncludeInvalidFieldPredicate(valueProviderResult.Values);
            }

            var model = new FieldQuery<T>(validValues, valueProviderResult.Values);
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}
