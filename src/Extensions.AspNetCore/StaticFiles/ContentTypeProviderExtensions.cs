using Microsoft.AspNetCore.StaticFiles;

namespace AspNetCoreNuxt.Extensions.AspNetCore.StaticFiles
{
    /// <summary>
    /// <see cref="IContentTypeProvider"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class ContentTypeProviderExtensions
    {
        /// <summary>
        /// 指定されたファイルパスをもとに MIME タイプを取得します。
        /// </summary>
        /// <param name="provider"><see cref="IContentTypeProvider"/> オブジェクト。</param>
        /// <param name="subpath">ファイルパス。</param>
        /// <returns>MIME タイプ。</returns>
        public static string GetContentType(this IContentTypeProvider provider, string subpath)
            => provider.GetContentType(subpath, defaultContentType: "application/octet-stream");

        /// <summary>
        /// 指定されたファイルパスをもとに MIME タイプを取得します。
        /// </summary>
        /// <param name="provider"><see cref="IContentTypeProvider"/> オブジェクト。</param>
        /// <param name="subpath">ファイルパス。</param>
        /// <param name="defaultContentType">MIME タイプを識別できなかった場合に使用する既定の MIME タイプ。</param>
        /// <returns>MIME タイプ。</returns>
        public static string GetContentType(this IContentTypeProvider provider, string subpath, string defaultContentType)
            => provider.TryGetContentType(subpath, out var contentType) ? contentType : defaultContentType;
    }
}
