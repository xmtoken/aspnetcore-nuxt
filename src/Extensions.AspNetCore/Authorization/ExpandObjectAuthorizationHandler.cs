using AspNetCoreNuxt.Extensions.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Dynamic;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Authorization
{
    /// <summary>
    /// <see cref="ExpandoObjectFactory"/> クラスで <see cref="ExpandoObject"/> オブジェクトに展開するプロパティを検証するハンドラーを表します。
    /// </summary>
    /// <typeparam name="TRequirement"><see cref="IAuthorizationRequirement"/> オブジェクトの型。</typeparam>
    /// <typeparam name="TRootResource">ルートコンポーネントのリソースの型。</typeparam>
    /// <typeparam name="TResource">リソースの型。</typeparam>
    public abstract class ExpandObjectAuthorizationHandler<TRequirement, TRootResource, TResource> : AuthorizationHandler<TRequirement, ExpandObjectAuthorizationResource<TRootResource, TResource>>
        where TRequirement : IAuthorizationRequirement
    {
    }
}
