using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace AspNetCoreNuxt.Extensions.DependencyInjection
{
    /// <summary>
    /// <see cref="IServiceCollection"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class ApplicationServiceCollectionExtensions
    {
        /// <summary>
        /// <see cref="IApplicationService"/> インターフェイスを実装したすべてのクラスをサービスへ追加します。
        /// </summary>
        /// <typeparam name="T">スキャンするアセンブリに含まれるオブジェクトの型。</typeparam>
        /// <param name="services"><see cref="IServiceCollection"/> オブジェクト。</param>
        /// <returns><see cref="IServiceCollection"/> オブジェクト。</returns>
        public static IServiceCollection AddApplicationServices<T>(this IServiceCollection services)
        {
            foreach (var type in typeof(T).Assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract))
            {
                if (typeof(IApplicationService).IsAssignableFrom(type))
                {
                    services.AddScoped(type);
                }
            }

            return services;
        }
    }
}
