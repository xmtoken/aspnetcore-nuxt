using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AspNetCoreNuxt.Extensions.DependencyInjection
{
    /// <summary>
    /// <see cref="IServiceCollection"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// <see cref="IDependencyInjectionService"/> インターフェイスを実装するすべてのクラスをサービスへ追加します。
        /// </summary>
        /// <typeparam name="T">スキャンするアセンブリに含まれるオブジェクトの型。</typeparam>
        /// <param name="services"><see cref="IServiceCollection"/> オブジェクト。</param>
        /// <returns><see cref="IServiceCollection"/> オブジェクト。</returns>
        public static IServiceCollection AddDependencyInjectionServices<T>(this IServiceCollection services)
        {
            foreach (var implementationType in typeof(T).Assembly.GetTypes().Where(x => x.IsClass && x.IsConcrete()))
            {
                if (implementationType.GetInterface(typeof(IDependencyInjectionService<>).Name) is Type interfaceType)
                {
                    services.AddScoped(interfaceType.GenericTypeArguments[0], implementationType);
                }
                else if (typeof(IDependencyInjectionService).IsAssignableFrom(implementationType))
                {
                    services.AddScoped(implementationType);
                }
            }
            return services;
        }
    }
}
