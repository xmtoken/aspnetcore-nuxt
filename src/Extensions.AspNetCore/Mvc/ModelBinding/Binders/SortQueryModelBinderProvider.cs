using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ModelBinding.Binders
{
    /// <summary>
    /// <see cref="SortQueryModelBinder{T}"/> クラスのプロバイダーを表します。
    /// </summary>
    public class SortQueryModelBinderProvider : IModelBinderProvider
    {
        /// <inheritdoc/>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType.IsGenericType &&
                context.Metadata.ModelType.GetGenericTypeDefinition() == typeof(ISortQuery<>))
            {
                var typeArguments = context.Metadata.ModelType.GenericTypeArguments;
                var modelBinderType = typeof(SortQueryModelBinder<>).MakeGenericType(typeArguments);
                var modelBinderConstructor = ActivatorHelper.GetConstructor(modelBinderType, typeof(ILoggerFactory));
                var loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
                return (IModelBinder)modelBinderConstructor.Invoke(new[] { loggerFactory });
            }
            return null;
        }
    }
}
