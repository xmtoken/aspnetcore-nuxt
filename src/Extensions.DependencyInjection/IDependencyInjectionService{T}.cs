using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreNuxt.Extensions.DependencyInjection
{
    /// <summary>
    /// <see cref="ServiceCollectionExtensions.AddDependencyInjectionServices{T}(IServiceCollection)"/> メソッドによって自動でサービスに登録されることを表します。
    /// </summary>
    /// <typeparam name="TService">サービスの型。</typeparam>
    public interface IDependencyInjectionService<TService> : IDependencyInjectionService
    {
    }
}
