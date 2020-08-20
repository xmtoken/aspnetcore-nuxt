using AspNetCoreNuxt.Applications.WebHost.Core.Mvc;
using AspNetCoreNuxt.Applications.WebHost.Features.Versioning.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

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
        [SuppressMessage("Performance", "CA1822:メンバーを static に設定します")]
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
