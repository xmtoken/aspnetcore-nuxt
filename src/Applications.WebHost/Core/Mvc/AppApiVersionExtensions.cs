using System;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Mvc
{
    /// <summary>
    /// <see cref="AppApiVersion"/> 列挙体の拡張機能を提供します。
    /// </summary>
    public static class AppApiVersionExtensions
    {
        /// <summary>
        /// API バージョン形式の文字列 {MajorVersion}.{MinorVersion} へ変換します。
        /// </summary>
        /// <param name="version"><see cref="AppApiVersion"/> 値。</param>
        /// <returns>API バージョン形式の文字列。</returns>
        public static string ToVersion(this AppApiVersion version)
        {
            var value = (int)version;
            var enumeration = Enum.Parse<AppApiVersion>(value.ToString());
            return enumeration.ToString().Replace("Version_", null).Replace("_", ".");
        }
    }
}
