using AspNetCoreNuxt.Extensions.AspNetCore.Logging;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ModelBinding.Binders
{
    /// <summary>
    /// <see cref="PagingQuery"/> クラスのモデルバインダーを表します。
    /// </summary>
    public class PagingQueryModelBinder : IModelBinder
    {
        /// <summary>
        /// <see cref="ILogger"/> オブジェクトを表します。
        /// </summary>
        private readonly ILogger Logger;

        /// <summary>
        /// <see cref="PagingQueryModelBinder"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> オブジェクト。</param>
        public PagingQueryModelBinder(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<PagingQueryModelBinder>();
        }

        /// <inheritdoc/>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var offsetValueProviderResult = bindingContext.ValueProvider.GetValue(PagingQuery.OffsetBindingModelName);
            var limitValueProviderResult = bindingContext.ValueProvider.GetValue(PagingQuery.LimitBindingModelName);

            var isValidOffset = int.TryParse(offsetValueProviderResult.Values, out var offset) && offset >= 0;
            var isValidLimit = int.TryParse(limitValueProviderResult.Values, out var limit) && limit > 0;

            if ((offsetValueProviderResult.Length > 0 && !isValidOffset) ||
                (limitValueProviderResult.Length > 0 && !isValidLimit))
            {
                Logger.IncludeInvalidPagingPredicate(offsetValueProviderResult.Values, limitValueProviderResult.Values);
            }

            var model = new PagingQuery(
                isValidOffset ? offset : null, offsetValueProviderResult.Values,
                isValidLimit ? limit : null, limitValueProviderResult.Values);
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}
