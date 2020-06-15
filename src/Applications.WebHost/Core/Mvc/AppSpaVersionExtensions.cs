using System;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Mvc
{
    /// <summary>
    /// <see cref="AppSpaVersion"/> 列挙体の拡張機能を提供します。
    /// </summary>
    public static class AppSpaVersionExtensions
    {
        /// <summary>
        /// SPA バージョン形式の文字列 {MajorVersion}.{MinorVersion} へ変換します。
        /// </summary>
        /// <param name="version"><see cref="AppSpaVersion"/> 値。</param>
        /// <returns>SPA バージョン形式の文字列。</returns>
        public static string ToVersion(this AppSpaVersion version)
        {
            var value = (int)version;
            var enumeration = Enum.Parse<AppSpaVersion>(value.ToString());
            return enumeration.ToString().Replace("Version_", null).Replace("_", ".");
        }
    }
}
