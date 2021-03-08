using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ModelBinding.Binders
{
    /// <summary>
    /// <see cref="AggregateQueryModelBinder{T}"/> クラスのプロバイダーを表します。
    /// </summary>
    public class AggregateQueryModelBinderProvider : IModelBinderProvider
    {
        /// <inheritdoc/>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType.IsGenericType &&
                context.Metadata.ModelType.GetGenericTypeDefinition() == typeof(IAggregateQuery<>))
            {
                var typeArguments = context.Metadata.ModelType.GenericTypeArguments;
                var modelBinderType = typeof(AggregateQueryModelBinder<>).MakeGenericType(typeArguments);
                var modelBinderConstructor = ActivatorHelper.GetConstructor(modelBinderType, typeof(ILoggerFactory));
                var loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
                return (IModelBinder)modelBinderConstructor.Invoke(new[] { loggerFactory });
            }
            return null;
        }
    }
}
