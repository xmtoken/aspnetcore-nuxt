using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ModelBinding.Binders
{
    /// <summary>
    /// <see cref="SearchConditionsModelBinder{T}"/> クラスのプロバイダーを表します。
    /// </summary>
    public class SearchConditionsModelBinderProvider : IModelBinderProvider
    {
        /// <inheritdoc/>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType.IsGenericType &&
                context.Metadata.ModelType.GetGenericTypeDefinition() == typeof(SearchConditions<>))
            {
                var typeArguments = context.Metadata.ModelType.GenericTypeArguments;
                var modelBinderType = typeof(SearchConditionsModelBinder<>).MakeGenericType(typeArguments);
                var modelBinderConstructor = ActivatorHelper.GetConstructor(modelBinderType, typeof(ILoggerFactory));
                var loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
                return (IModelBinder)modelBinderConstructor.Invoke(new[] { loggerFactory });
            }
            return null;
        }
    }
}
