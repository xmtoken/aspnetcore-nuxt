using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ModelBinding
{
    /// <summary>
    /// <see cref="IModelBinderProvider"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class ModelBinderProviderExtensions
    {
        /// <summary>
        /// クエリのモデルバインダーのプロバイダーを追加します。
        /// </summary>
        /// <param name="providers"><see cref="IModelBinderProvider"/> オブジェクトのコレクション。</param>
        public static void AddQueryModelBinderProviders(this IList<IModelBinderProvider> providers)
        {
            providers.Insert(0, new AggregateQueryModelBinderProvider());
            providers.Insert(0, new FieldQueryModelBinderProvider());
            providers.Insert(0, new GroupQueryModelBinderProvider());
            providers.Insert(0, new PagingQueryModelBinderProvider());
            providers.Insert(0, new SearchConditionsModelBinderProvider());
            providers.Insert(0, new SortQueryModelBinderProvider());
        }
    }
}
