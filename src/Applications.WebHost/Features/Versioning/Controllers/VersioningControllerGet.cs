using AspNetCoreNuxt.Applications.WebHost.Core.Mvc;
using AspNetCoreNuxt.Applications.WebHost.Features.Versioning.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Versioning.Controllers
{
    public partial class VersioningController
    {
        /// <summary>
        /// アプリケーションのバージョン情報を取得します。
        /// </summary>
        /// <returns><see cref="AppVersion"/> オブジェクト。</returns>
        [ApiVersionNeutral]
        [HttpGet]
        public AppVersion Get()
        {
            return new AppVersion()
            {
                ApiVersion = AppApiVersion.Latest.ToVersion(),
                SpaVersion = AppSpaVersion.Latest.ToVersion(),
            };
        }
    }
}
