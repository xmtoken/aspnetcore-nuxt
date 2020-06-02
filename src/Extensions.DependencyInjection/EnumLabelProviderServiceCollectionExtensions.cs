using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace AspNetCoreNuxt.Extensions.DependencyInjection
{
    /// <summary>
    /// <see cref="IServiceCollection"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class EnumLabelProviderServiceCollectionExtensions
    {
        /// <summary>
        /// <see cref="IEnumLabelProvider"/> インターフェイスに関するクラスをサービスへ追加します。
        /// </summary>
        /// <typeparam name="T">スキャンするアセンブリに含まれるオブジェクトの型。</typeparam>
        /// <param name="services"><see cref="IServiceCollection"/> オブジェクト。</param>
        /// <returns><see cref="IServiceCollection"/> オブジェクト。</returns>
        public static IServiceCollection AddEnumLabelProviders<T>(this IServiceCollection services)
        {
            services.AddScoped<IEnumLabelProviderFactory>(provider => new EnumLabelProviderFactory(provider));

            foreach (var type in typeof(T).Assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract))
            {
                if (typeof(IEnumLabelProvider).IsAssignableFrom(type))
                {
                    var genericInterfaceTypes = type.GetInterfaces().Where(x => x.IsGenericType);
                    var serviceType = genericInterfaceTypes.Single(x => x.GetGenericTypeDefinition() == typeof(IEnumLabelProvider<>));
                    services.AddScoped(serviceType, type);
                }
            }

            return services;
        }
    }
}
