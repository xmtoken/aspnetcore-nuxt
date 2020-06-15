using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Mvc
{
    /// <inheritdoc/>
    public class AppApiVersionAttribute : ApiVersionAttribute
    {
        /// <summary>
        /// <see cref="ApiVersionAttribute"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="version">API バージョンを表す <see cref="AppApiVersion"/> 値。</param>
        public AppApiVersionAttribute(AppApiVersion version)
            : base(version.ToVersion())
        {
        }
    }
}
