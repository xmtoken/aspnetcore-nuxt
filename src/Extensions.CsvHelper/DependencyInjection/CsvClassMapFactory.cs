using CsvHelper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCoreNuxt.Extensions.CsvHelper.DependencyInjection
{
    /// <inheritdoc cref="ICsvClassMapFactory"/>
    internal class CsvClassMapFactory : ICsvClassMapFactory
    {
        /// <summary>
        /// <see cref="IServiceProvider"/> オブジェクトを表します。
        /// </summary>
        private readonly IServiceProvider ServiceProvider;

        /// <summary>
        /// <see cref="CsvClassMapFactory"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/> オブジェクト。</param>
        public CsvClassMapFactory(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public TClassMap CreateClassMap<TClassMap>()
            where TClassMap : ClassMap
            => ServiceProvider.GetRequiredService<TClassMap>();
    }
}
