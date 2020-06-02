using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore.Metadata
{
    /// <summary>
    /// <see cref="IServiceCollection"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class EntityMetadataServiceCollectionExtensions
    {
        /// <summary>
        /// <see cref="IEntityMetadataProvider"/> インターフェイスに関するクラスをサービスへ追加します。
        /// </summary>
        /// <typeparam name="TContext">メタデータを取得する <see cref="DbContext"/> クラスの型。</typeparam>
        /// <param name="services"><see cref="IServiceCollection"/> オブジェクト。</param>
        /// <returns><see cref="IServiceCollection"/> オブジェクト。</returns>
        public static IServiceCollection AddEntityMetadataProvider<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            services.AddScoped<IEntityMetadataProvider, EntityMetadataProvider<TContext>>();
            return services;
        }
    }
}
