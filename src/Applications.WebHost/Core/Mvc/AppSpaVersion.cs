using System.Diagnostics.CodeAnalysis;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Mvc
{
    /// <summary>
    /// SPA バージョンを表します。
    /// </summary>
    [SuppressMessage("Naming", "CA1707:識別子はアンダースコアを含むことはできません")]
    public enum AppSpaVersion
    {
        /// <summary>
        /// バージョン 1.0 を表します。
        /// </summary>
        Version_1_0,

        /// <summary>
        /// 最新バージョンを表します。
        /// </summary>
        Latest = Version_1_0,
    }
}
