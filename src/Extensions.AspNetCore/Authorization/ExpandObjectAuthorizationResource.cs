namespace AspNetCoreNuxt.Extensions.AspNetCore.Authorization
{
    /// <summary>
    /// <see cref="ExpandObjectAuthorizationHandler{TRequirement, TRootResource, TResource}"/> クラスで検証するリソースを表します。
    /// </summary>
    /// <typeparam name="TRootResource">ルートコンポーネントのリソースの型。</typeparam>
    /// <typeparam name="TResource">リソースの型。</typeparam>
    public class ExpandObjectAuthorizationResource<TRootResource, TResource>
    {
        /// <summary>
        /// ルートコンポーネントのリソースを表す <typeparamref name="TRootResource"/> オブジェクトを取得します。
        /// </summary>
        public TRootResource RootResource { get; }

        /// <summary>
        /// リソースを表す <typeparamref name="TResource"/> オブジェクトを取得します。
        /// </summary>
        public TResource Resource { get; }

        /// <summary>
        /// プロパティ名を取得します。
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// <see cref="ExpandObjectAuthorizationResource{TRootResource, TResource}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="rootResource">ルートコンポーネントのリソースを表す <typeparamref name="TRootResource"/> オブジェクト。</param>
        /// <param name="resource">リソースを表す <typeparamref name="TResource"/> オブジェクト。</param>
        /// <param name="propertyName">プロパティ名。</param>
        public ExpandObjectAuthorizationResource(TRootResource rootResource, TResource resource, string propertyName)
        {
            RootResource = rootResource;
            Resource = resource;
            PropertyName = propertyName;
        }
    }
}
