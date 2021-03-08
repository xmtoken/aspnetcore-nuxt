using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ModelBinding.Binders
{
    /// <summary>
    /// <see cref="PagingQueryModelBinder"/> クラスのプロバイダーを表します。
    /// </summary>
    public class PagingQueryModelBinderProvider : IModelBinderProvider
    {
        /// <inheritdoc/>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(IPagingQuery))
            {
                var loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
                return new PagingQueryModelBinder(loggerFactory);
            }
            return null;
        }
    }
}
