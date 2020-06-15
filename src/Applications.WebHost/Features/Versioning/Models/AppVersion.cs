namespace AspNetCoreNuxt.Applications.WebHost.Features.Versioning.Models
{
    /// <summary>
    /// アプリケーションのバージョンを表します。
    /// </summary>
    public class AppVersion
    {
        /// <summary>
        /// API バージョンを取得または設定します。
        /// </summary>
        public string ApiVersion { get; set; }

        /// <summary>
        /// SPA バージョンを取得または設定します。
        /// </summary>
        public string SpaVersion { get; set; }
    }
}
