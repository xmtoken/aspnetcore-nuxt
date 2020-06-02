using CsvHelper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace AspNetCoreNuxt.Extensions.CsvHelper.DependencyInjection
{
    /// <summary>
    /// <see cref="IServiceCollection"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class CsvHelperServiceCollectionExtensions
    {
        /// <summary>
        /// <see cref="global::CsvHelper"/> ライブラリで依存関係の解決を行うためのクラスをサービスへ追加します。
        /// </summary>
        /// <typeparam name="T">スキャンするアセンブリに含まれるオブジェクトの型。</typeparam>
        /// <param name="services"><see cref="IServiceCollection"/> オブジェクト。</param>
        /// <returns><see cref="IServiceCollection"/> オブジェクト。</returns>
        public static IServiceCollection AddCsvHelper<T>(this IServiceCollection services)
        {
            services.AddScoped<ICsvClassMapFactory>(provider => new CsvClassMapFactory(provider));

            foreach (var type in typeof(T).Assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract))
            {
                if (typeof(ClassMap).IsAssignableFrom(type))
                {
                    services.AddScoped(type);
                }
            }

            return services;
        }
    }
}
