using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCoreNuxt.Extensions.DependencyInjection
{
    /// <inheritdoc cref="IEnumLabelProviderFactory"/>
    internal class EnumLabelProviderFactory : IEnumLabelProviderFactory
    {
        /// <summary>
        /// <see cref="IServiceProvider"/> オブジェクトを表します。
        /// </summary>
        private readonly IServiceProvider ServiceProvider;

        /// <summary>
        /// <see cref="EnumLabelProviderFactory"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/> オブジェクト。</param>
        public EnumLabelProviderFactory(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public IEnumLabelProvider CreateProvider(Type type)
        {
            var serviceType = typeof(IEnumLabelProvider<>).MakeGenericType(type);
            return (IEnumLabelProvider)ServiceProvider.GetRequiredService(serviceType);
        }
    }
}
